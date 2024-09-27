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
        //[Authorize] // Somente usuários autenticados podem fazer o download
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

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileExtension = Path.GetExtension(filePath);
            var mimeType = GetMimeType(fileExtension);
            return File(fileBytes, mimeType, Path.GetFileName(filePath));
        }

        private string GetMimeType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".pdf": return "application/pdf";
                case ".txt": return "text/plain";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default: return "application/octet-stream"; 
            }
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
            var usuarioId = documentoDto.UsuarioId;

            // Verifica se o ID do usuário é válido
            if (usuarioId <= 0)
            {
                return BadRequest("ID do usuário inválido.");
            }
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
            documento.Status = Status.Ativo;
            documento.UsuarioId = usuarioId; // Associar o usuário logado


            // Salva as alterações no banco de dados
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();

            return Ok(new {documento.Nome, documento.Categoria, documento.VersaoAtual, documento.Status, documento.UsuarioId });
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