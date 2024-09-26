using System.Security.Claims;
using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ApiCrud.Documentos
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, [FromForm] DocumentosDTO documentoDto)
        {
            //var user = HttpContext.User;

            // Verifica se o usuário está autenticado
            /*if (!user.Identity?.IsAuthenticated == true)
            {
                return Unauthorized();
            }*/

            // Obtém o ID do usuário autenticado usando o Claim
            /*var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return BadRequest("Usuário não autenticado.");
            }*/
            var usuarioId = documentoDto.UsuarioId;

            // Verifica se o ID do usuário é válido
            if (usuarioId <= 0)
            {
                return BadRequest("ID do usuário inválido.");
            }

            //int usuarioId = int.Parse(userIdClaim.Value);

            if (files == null || files.Count == 0)
            {
                return BadRequest("Você deve enviar exatamente um arquivo.");
            }

            var formFile = files.First();
            if (formFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "DocumentosUploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(formFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Criar o documento a partir do DTO
                var documento = new Documento
                {
                    Nome = documentoDto.Nome,
                    Descricao = documentoDto.Descricao,
                    Categoria = documentoDto.Categoria,
                    VersaoAtual = documentoDto.Versaoatual,
                    Caminho = filePath,
                    DataUpload = DateTime.UtcNow,
                    Status = Status.Ativo,
                    UsuarioId = usuarioId // Associar o usuário logado
                };

                _context.Documentos.Add(documento);
                await _context.SaveChangesAsync();

                return Ok(new { documento.IdDocumento, documento.Nome, caminho = documento.Caminho, documento.UsuarioId });
            }

            return BadRequest("Arquivo inválido.");
        }
        [HttpGet("download/{id}")]
        [Authorize] // Somente usuários autenticados podem fazer o download
        public async Task<IActionResult> Download(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null )
            {
                return NotFound();
            }

            // O caminho do arquivo foi salvo no banco de dados
            var filePath = documento.Caminho;

            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Arquivo não encontrado.");
            }

            // Retorna o arquivo para download
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }


        // Outros métodos (delete, inativar, etc.) permanecem os mesmos, sem alterações.
        // Exemplo de GET
        [HttpGet("visualizar")]
        public async Task<IActionResult> Visualizar([FromServices] AppDbContext context, int pageNumber = 1, int pageSize = 10)
        {
            // Paginação: salta e pega apenas um número limitado de resultados
            var documentos = await context.Documentos
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            return Ok(documentos);
        }

        [HttpPut("editar/{id}")]
        //[Authorize(Roles = "Administrador")]// Somente usuários autenticados podem editar
        public async Task<IActionResult> Editar(int id, [FromBody] DocumentosDTO documentoDto)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            // Atualiza os campos do documento com base no DTO
            documento.Nome = documentoDto.Nome;
            documento.Descricao = documentoDto.Descricao;
            documento.Categoria = documentoDto.Categoria;
            documento.VersaoAtual = documentoDto.Versaoatual;

            // Salva as alterações no banco de dados
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();

            return Ok(new { documento.IdDocumento, documento.Nome, documento.Categoria, documento.VersaoAtual });
        }

        [HttpDelete("deletar/{id}")]
        //[Authorize(Roles = "Administrador")] // Somente administradores podem deletar
        public async Task<IActionResult> Deletar(int id)
        {
            var papel = HttpContext.Request.Headers["Papel"].ToString();

            // Verifica se o papel é "Administrador"
            if (papel != "Administrador")
            {
                return Forbid("Acesso negado. Somente administradores podem deletar.");
            }
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }
            // Verifica se o arquivo existe no sistema de arquivos
            var filePath = documento.Caminho;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath); // Deleta o arquivo fisicamente
            }

            // Remove o documento do banco de dados
            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();

            return Ok("Documento deletado com sucesso.");
        }

        [HttpPut("inativar/{id}")]
        [Authorize(Roles = "Administrador")] // Somente administradores podem inativar
        public async Task<IActionResult> Inativar(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            // Marca o documento como inativo
            documento.Status = Status.Inativo;
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();

            return Ok("Documento inativado com sucesso.");
        }
    }
}