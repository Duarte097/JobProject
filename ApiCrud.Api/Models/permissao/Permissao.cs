using System;
using System.Collections.Generic;
using ApiCrud.Models;

namespace ApiCrud.Models;

public  class Permissao
{
    public int IdPermissao { get; set; }

    public int UsuarioId { get; set; }

    public int DocumentoId { get; set; }

    public string? Permissaotipo { get; set; }

    public virtual Documento Documento { get; set; } = null!;

    public virtual UsuarioModel Usuario { get; set; } = null!;

    public Permissao(string permissaotipo)
    {
        this.Permissaotipo = permissaotipo;
    }
    public Permissao() { }
}
