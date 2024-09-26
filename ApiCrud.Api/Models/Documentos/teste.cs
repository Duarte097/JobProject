/*using System.Security.Claims;
using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize] // Garante que o usuário está autenticado
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, [FromForm] Documento documento)
        {
            var user = HttpContext.User;

            // Verifica se o usuário está autenticado
            if (!user.Identity?.IsAuthenticated == true)
            {
                return Unauthorized();
            }

            // Obtém o ID do usuário das claims
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int usuarioId))
            {
                return BadRequest("ID do usuário inválido.");
            }

            if (files == null || files.Count == 0 || files.Count > 1)
            {
                return BadRequest("Você deve enviar exatamente um arquivo.");
            }

            var formFile = files.First();
            if (formFile.Length > 0)
            {
                // Pasta onde os arquivos serão armazenados no servidor (exemplo no GitHub Pages)
                var uploadsFolder = Path.Combine("https://duarte097.github.io/JobProject/"); // Caminho do GitHub Pages
                var fileName = Path.GetRandomFileName(); // Gerar um nome de arquivo aleatório para evitar conflitos
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Salvar o arquivo localmente antes de enviar para o servidor
                var localFilePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
                using (var stream = new FileStream(localFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Salvar informações do documento no banco de dados
                documento.Caminho = filePath; // Caminho onde o arquivo será acessado no GitHub Pages
                documento.DataUpload = DateTime.UtcNow; // Atualiza a data de upload
                documento.Status = Status.Ativo; // Define como Ativo por padrão
                documento.UsuarioId = usuarioId; // Associa o documento ao usuário que fez o upload

                _context.Documentos.Add(documento);
                await _context.SaveChangesAsync();

                return Ok(new { documento.IdDocumento, documento.Nome, caminho = documento.Caminho });
            }

            return BadRequest("Arquivo inválido.");
        }

        [HttpGet("download/{id}")]
        [Authorize] // Somente usuários autenticados podem fazer o download
        public async Task<IActionResult> Download(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null || documento.Status != Status.Ativo)
            {
                return NotFound();
            }

            // O caminho do arquivo já está no formato acessível via GitHub Pages
            var filePath = documento.Caminho;
            return Ok(new { caminho = filePath });
        }

        // Outros métodos (delete, inativar, etc.) permanecem os mesmos, sem alterações.
    }
}*/
