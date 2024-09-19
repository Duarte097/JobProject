using ApiCrud.Api.Models;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; } 
        public DbSet<Permissao> Permissao { get; set; } 
        public DbSet<Versaodocumento> VersaoDoc { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;database=digitaldoc;user=root;password=1234",
                    new MySqlServerVersion(new Version(8, 0, 36))
                );
            }
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exemplo de configuração adicional
            modelBuilder.Entity<Documento>().ToTable("Documentos");
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios");
            modelBuilder.Entity<Permissao>().ToTable("Permissao");
            modelBuilder.Entity<Versaodocumento>().ToTable("Versaodocumento");

            // Chaves primárias
            modelBuilder.Entity<Documento>().HasKey(d => d.IdDocumento);
            modelBuilder.Entity<UsuarioModel>().HasKey(u => u.IdUsuarios);
            modelBuilder.Entity<Permissao>().HasKey(p => p.IdPermissao);
            modelBuilder.Entity<Versaodocumento>().HasKey(v => v.IdVersaodocumento);

            // Relacionamento Documento -> Usuario
            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Usuario) // Documento tem um Usuário
                .WithMany(u => u.Documentos) // Usuário pode ter muitos Documentos
                .HasForeignKey(d => d.UsuarioId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata ao deletar o usuário

            // Relacionamento VersaoDocumento -> Documento
            modelBuilder.Entity<Versaodocumento>()
                .HasOne(v => v.Documento) // Versão de Documento pertence a um Documento
                .WithMany(d => d.Versaodocumento) // Documento pode ter várias versões
                .HasForeignKey(v => v.DocumentoId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata ao deletar o documento

            // Relacionamento Permissao -> Usuario e Documento
            modelBuilder.Entity<Permissao>()
                .HasOne(p => p.Usuario) // Permissão pertence a um Usuário
                .WithMany(u => u.Permissoes) // Usuário pode ter várias permissões
                .HasForeignKey(p => p.UsuarioId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata ao deletar o usuário

            modelBuilder.Entity<Permissao>()
                .HasOne(p => p.Documento) // Permissão pertence a um Documento
                .WithMany(d => d.Permissoes) // Documento pode ter várias permissões
                .HasForeignKey(p => p.DocumentoId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata ao deletar o documento  
        }
    }
}
