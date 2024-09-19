using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiCrud.Models;

namespace ApiCrud.Models;

public  class Permissao
{
    [Key]
    public int IdPermissao { get; set; }

    [ForeignKey("Usuarios")]
    public int UsuarioId { get; set; }

    [ForeignKey("Documentos")]
    public int DocumentoId { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Permissaotipo { get; set; }

    public Documento Documento { get; set; } = null!;

    public UsuarioModel Usuario { get; set; } = null!;

    public Permissao(string permissaotipo, int usuarioid, int documentoid)
    {
        this.Permissaotipo = permissaotipo;
        this.UsuarioId = usuarioid;
        this.DocumentoId = documentoid;
    }
    public Permissao() { }
}
