using ApiCrud.Api.Models;
using ApiCrud.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VersaoDocRotas : ControllerBase
{
    private readonly AppDbContext _context;

    public VersaoDocRotas(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> CriarVersao([FromBody] Versaodocumento novaVersao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        novaVersao.Dataversao = DateTime.Now;
        _context.VersaoDoc.Add(novaVersao);
        await _context.SaveChangesAsync();

        return Ok(novaVersao);
    }

    [HttpGet("visualizar/{id}")]
    public async Task<IActionResult> VisualizarVersao(int id)
    {
        var versao = await _context.VersaoDoc.FindAsync(id);
        if (versao == null)
            return NotFound();

        return Ok(versao);
    }

    // Mais endpoints para atualizar, deletar ou visualizar várias versões...
}
