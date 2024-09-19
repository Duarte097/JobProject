using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiCrud.Models;

namespace ApiCrud.Api.Models;

public partial class Versaodocumento
{
    [Key]
    public int IdVersaodocumento { get; set; }

    [ForeignKey("Documentos")]
    public int DocumentoId { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Numeroversao { get; set; }

    public DateTime? Dataversao { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Caminhoversaoarquivo { get; set; }

    [MaxLength(255)]
    public string? Descricao { get; set; }

    public Documento Documento { get; set; } = null!;

    public Versaodocumento(string numeroversao,int documentoid, DateTime dataversao,string caminhoversaoarquivo, string descricao)
    {
        this.Numeroversao = numeroversao;
        this.DocumentoId = documentoid;
        this.Dataversao = dataversao;
        this.Caminhoversaoarquivo = caminhoversaoarquivo;
        this.Descricao = descricao;

    }
    public Versaodocumento() { }
}
