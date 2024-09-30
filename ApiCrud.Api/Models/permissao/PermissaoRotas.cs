using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PermissaoRotas : ControllerBase
{
    private readonly AppDbContext _context;

    public PermissaoRotas(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("atribuir")]
    public async Task<IActionResult> AtribuirPermissao([FromBody] Permissao permissao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Permissao.Add(permissao);
        await _context.SaveChangesAsync();

        return Ok(permissao);
    }

    [HttpGet("usuario/{usuarioId}/documento/{documentoId}")]
    public async Task<IActionResult> ObterPermissao(int usuarioId, int documentoId)
    {
        var permissao = await _context.Permissao
            .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.DocumentoId == documentoId);

        if (permissao == null)
            return NotFound();

        return Ok(permissao);
    }


}
