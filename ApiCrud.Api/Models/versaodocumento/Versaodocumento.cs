using System;
using System.Collections.Generic;
using ApiCrud.Models;

namespace ApiCrud.Api.Models;

public partial class Versaodocumento
{
    public int IdVersaodocumento { get; set; }

    public int DocumentoId { get; set; }

    public string? Numeroversao { get; set; }

    public DateTime? Dataversao { get; set; }

    public string? Caminhoversaoarquivo { get; set; }

    public string? Descricao { get; set; }

    public virtual Documento Documento { get; set; } = null!;

    public Versaodocumento(string numeroversao, DateTime dataversao,string caminhoversaoarquivo, string descricao)
    {
        this.Numeroversao = numeroversao;
        this.Dataversao = dataversao;
        this.Caminhoversaoarquivo = caminhoversaoarquivo;
        this.Descricao = descricao;

    }
    public Versaodocumento() { }
}
