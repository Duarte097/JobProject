using System.ComponentModel.DataAnnotations;
using ApiCrud.Api.Models;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Models
{
    public class UsuarioModel
    {
        [Key]
        public int IdUsuarios { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(255)]
        //[Index(IsUnique = true)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? SenhaHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Papel { get; set; }
        public DateTime? Datacriacao { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public virtual ICollection<Permissao> Permissoes { get; set; } = new List<Permissao>();

        public UsuarioModel(string nome, string email, string senhaHash, string papel, DateTime datacriacao) 
        {
            this.Nome = nome;
            this.Email = email;
            this.SenhaHash = senhaHash;
            this.Papel = papel;
            this.Datacriacao = datacriacao;
        }
        public UsuarioModel() { }
    }
}
