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
                    Caminho = filePath, // Corrigido para CaminhoArquivo
                    DataUpload = DateTime.Now,
                    VersaoAtual = form["versao"],
                    Categoria = form["categoria"],
                    Status = Status.Ativo, // Definindo status como Ativo
                    UsuarioId = usuarioId
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
            rotasdocumentos.MapGet("", async (AppDbContext context) => {
                var documentos = await context.Documentos.ToListAsync();
                return Results.Ok(documentos);
            });

            // Update Documento
            rotasdocumentos.MapPut("{id}", async (int id, UpdateDocumentos request, AppDbContext context, HttpContext httpContext, CancellationToken ct) => 
            {
                var user = httpContext.User;

                // Verificar se o usuário está autenticado
                if (!user.Identity.IsAuthenticated)
                {
                    return Results.Unauthorized(); // Retorna 401 se o usuário não estiver autenticado
                }

                // Obter o ID do usuário das claims
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Results.BadRequest("Usuário não identificado.");
                }

                // Buscar o usuário no banco de dados
                var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.IdUsuarios == int.Parse(userIdClaim), ct);

                if (usuario == null)
                {
                    return Results.NotFound("Usuário não encontrado.");
                }

                var documento = await context.Documentos.SingleOrDefaultAsync(d => d.IdDocumento == id, ct);

                if (documento == null)
                {
                    return Results.NotFound();
                }

                // Se o usuário não for administrador, verificar se ele é o dono do documento
                if (usuario.Papel != "Administrador" && documento.UsuarioId != usuario.IdUsuarios)
                {
                    return Results.Forbid(); // Retorna 403 se não for administrador e não for o dono do documento
                }

                // Atualiza todos os atributos
                documento.Nome = request.Nome;
                documento.Descricao = request.Descricao;
                documento.Status = request.Status;
                documento.Caminho = request.Caminho;
                documento.VersaoAtual = request.Versaoatual;
                documento.Categoria = request.Categoria;
    
                await context.SaveChangesAsync(ct);
                return Results.Ok(new DocumentosDTO(documento.IdDocumento, documento.Nome, documento.Descricao,documento.Status, documento.VersaoAtual, documento.Categoria));
            });


            //Delete
            rotasdocumentos.MapDelete("{id}", async (int id, AppDbContext context, HttpContext httpContext, CancellationToken ct) =>
            {
                var user = httpContext.User;

                // Verificar se o usuário está autenticado
                if (!user.Identity.IsAuthenticated)
                {
                    return Results.Unauthorized(); // Retorna 401 se o usuário não estiver autenticado
                }

                // Obter o ID do usuário das claims
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Results.BadRequest("Usuário não identificado.");
                }

                // Buscar o usuário no banco de dados
                var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.IdUsuarios == int.Parse(userIdClaim), ct);

                if (usuario == null)
                {
                    return Results.NotFound("Usuário não encontrado.");
                }

                // Verificar se o usuário é um administrador
                if (usuario.Papel != "Administrador")
                {
                    return Results.Forbid(); // Retorna 403 se o usuário não for administrador
                }

                var documento = await context.Documentos.SingleOrDefaultAsync(d => d.IdDocumento == id, ct);

                if (documento == null)
                {
                    return Results.NotFound();
                }

                documento.Status = Status.Inativo; // Desativando o documento
                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });

        }
    }
}
