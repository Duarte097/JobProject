using System.Security.Claims;
using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Documentos
{
    public static class DocumentosRotas
    {
        public static void AddRotasDocumentos(this WebApplication app)
        {
            var rotasdocumentos = app.MapGroup("documento");

            // Endpoint para Upload de Documento
            rotasdocumentos.MapPost("upload", async (HttpRequest request, AppDbContext context, HttpContext httpContext) => {
                
                // Obter o usuário autenticado
                var user = httpContext.User;

                // Verificar se o usuário está autenticado
                if (!user.Identity.IsAuthenticated)
                {
                    return Results.Unauthorized(); // Retorna 401 se o usuário não estiver autenticado
                }

                // Obter o ID ou email do usuário das claims
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Results.BadRequest("Usuário não identificado.");
                }

                // Converter o ID do usuário para inteiro
                if (!int.TryParse(userIdClaim, out int usuarioId))
                {
                    return Results.BadRequest("ID do usuário inválido.");
                }

                var form = await request.ReadFormAsync();
                var file = form.Files.GetFile("file");

                if (file == null || file.Length == 0)
                {
                    return Results.BadRequest("Nenhum arquivo foi enviado.");
                }

                // Diretório onde os arquivos serão armazenados
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Gerar um nome de arquivo único para evitar colisões
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Salvar o arquivo no servidor
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Criar um novo documento no banco de dados com o caminho do arquivo
                var novoDocumento = new Documento
                {
                    Nome = form["nome"],
                    Descricao = form["descricao"],
                    Caminho = filePath, // Caminho do arquivo
                    DataUpload = DateTime.Now,
                    VersaoAtual = form["versao"],
                    Categoria = form["categoria"],
                    UsuarioId = usuarioId // ID do usuário logado obtido do token
                };

                context.Documentos.Add(novoDocumento);
                await context.SaveChangesAsync();

                return Results.Created($"/documentos/{novoDocumento.IdDocumento}", novoDocumento);
            });

            // Endpoint para Download de Documento (mantido o mesmo)
            rotasdocumentos.MapGet("download/{id}", async (int id, AppDbContext context) => {
                var documento = await context.Documentos.FindAsync(id);

                if (documento == null)
                {
                    return Results.NotFound();
                }

                var filePath = documento.Caminho;

                if (!System.IO.File.Exists(filePath))
                {
                    return Results.NotFound("Arquivo não encontrado no servidor.");
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileName = Path.GetFileName(filePath);

                // Retornar o arquivo para o cliente
                return Results.File(fileBytes, "application/octet-stream", fileName);
            });

            // Exemplo de GET
            rotasdocumentos.MapGet("{id}", async (int id, AppDbContext context) => {
                var documentos = await context.Documentos.FindAsync(id);
                return documentos is not null ? Results.Ok(documentos) : Results.NotFound();
            });

            //Update Documento
            rotasdocumentos.MapPut("{id}", async (int id, UpdateDocumentos request, AppDbContext context, HttpContext httpContext, CancellationToken ct) => 
            {
                // Obter as claims do usuário autenticado
                var user = httpContext.User;

                // Verificar se o usuário está autenticado
                if (!user.Identity.IsAuthenticated)
                {
                    return Results.Unauthorized(); // Retorna 401 se o usuário não estiver autenticado
                }

                // Obter o email ou o ID do usuário a partir das claims do token JWT
                var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return Results.BadRequest("Usuário não identificado.");
                }

                // Buscar o usuário no banco de dados com base no email ou ID
                var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.Email == userEmail, ct);

                if (usuario == null)
                {
                    return Results.NotFound("Usuário não encontrado.");
                }

                // Verificar se o papel do usuário é "Administrador"
                if (usuario.Papel != "Administrador")
                {
                    return Results.Forbid(); // Retorna 403 se o usuário não for administrador
                }

                var documento = await context.Documentos.SingleOrDefaultAsync(documento => documento.IdDocumento == id, ct);

                if(documento == null)
                    return Results.NotFound();
                
                documento.Nome = request.Nome;
                documento.Descricao =  request.Descricao;
                documento.Caminho = request.Caminho;
                documento.VersaoAtual =  request.Versaoatual;
                documento.Categoria = request.Categoria;

                await context.SaveChangesAsync(ct);
                return Results.Ok(new DocumentosDTO(documento.IdDocumento, documento.Nome, documento.Descricao, documento.VersaoAtual, documento.Categoria));
            });

            //Delete
            rotasdocumentos.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var documento = await context.Documentos.SingleOrDefaultAsync(documento => documento.IdDocumento == id, ct);

                if(documento == null)
                    return Results.NotFound();

                documento.Desativar();

                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });
        }
    }
}
