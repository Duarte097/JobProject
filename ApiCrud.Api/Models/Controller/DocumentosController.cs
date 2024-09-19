/*using ApiCrud.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/documentos")]
public class DocumentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DocumentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDocumentos()
    {
        // Lógica para listar todos os documentos
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocumento(int id)
    {
        // Lógica para obter detalhes de um documento específico
    }

    [HttpPost]
    public async Task<IActionResult> PostDocumento([FromForm] DocumentoUploadModel model)
    {
        // Lógica para fazer upload de um novo documento
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDocumento(int id, [FromBody] DocumentoUpdateModel model)
    {
        // Lógica para atualizar um documento existente (gera uma nova versão)
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocumento(int id)
    {
        // Lógica para excluir um documento (somente administradores)
    }

    [HttpPost("{id}/download")]
    public async Task<IActionResult> DownloadDocumento(int id)
    {
        // Lógica para fazer download de um documento
    }
}*/
