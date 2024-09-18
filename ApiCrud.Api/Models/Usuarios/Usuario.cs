using ApiCrud.Api.Models;
using ApiCrud.Models;

namespace ApiCrud.Models
{
    public class UsuarioModel
    {
        public int IdUsuarios { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? SenhaHash { get; set; }
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
