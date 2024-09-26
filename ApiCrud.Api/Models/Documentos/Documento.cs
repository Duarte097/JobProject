using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiCrud.Api.Models;
using System.ComponentModel;

namespace ApiCrud.Models 
{
    public enum Status
    {
        Ativo = 0,
        Inativo = 1
    }
    
    public class Documento
    {
        [Key]
        public int IdDocumento { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Nome { get; set; }

        [MaxLength(255)]
        public string? Descricao { get; set; }

        [MaxLength(100)]
        public string? Categoria { get; set; }

        public DateTime DataUpload { get; set; }

        [MaxLength(500)]
        public string? Caminho { get; set; }

        [MaxLength(10)]
        public string? VersaoAtual { get; set; }

        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; } // O usuário será setado automaticamente na controller, sem precisar ser passado na requisição.

        [Required]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public UsuarioModel Usuario { get; set; } = null!;

        public ICollection<Versaodocumento> Versaodocumento { get; set; } = new List<Versaodocumento>();
        public ICollection<Permissao> Permissoes { get; set; } = new List<Permissao>();

        public Documento(string nome, string descricao, DateTime dataUpload, string caminho, string versaoatual, string categoria)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.DataUpload = dataUpload;
            this.Status = Status.Ativo;
            this.Caminho = caminho;
            this.VersaoAtual = versaoatual;
            this.Categoria = categoria;
            // Removemos a associação do usuário aqui, pois será definido na camada Controller
        }

        public Documento() { }
    }
}