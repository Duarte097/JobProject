using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiCrud.Api.Models;

namespace ApiCrud.Models 
{
    public enum Status
    {
        Ativo,
        Inativo
    }
    
    public class Documento
    {
        public int id_documento { get; init; }
        public string Nome { get; private set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; } 
        public DateTime DataUpload { get; set; }
        public Status Status { get; private set; }
        public string Caminho { get; set; }
        public string Versao { get; set; }
        
        public virtual ICollection<Permissao> Permisaos { get; set; } = new List<Permissao>();

        public virtual UsuarioModel Usuario { get; set; } = null!;

        public virtual ICollection<Versaodocumento> Versaodocumentos { get; set; } = new List<Versaodocumento>();

        public Documento(int id_documento, string nome, string descricao, DateTime dataUpload, string caminho, string versao, string categoria)
        {
            this.Nome = nome;
            this.id_documento = id_documento;
            this.Descricao = descricao;
            this.DataUpload = dataUpload;
            this.Status = Status.Ativo;
            this.Caminho = caminho;
            this.Versao = versao;
            this.Categoria = categoria; 
        }
        
        public void Inativar()
        {
            this.Status = Status.Inativo;
        }

        public void Ativar()
        {
            this.Status = Status.Ativo;
        }
        
    }
}

