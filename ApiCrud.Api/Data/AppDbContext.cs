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

        // Se você tiver configurações adicionais, como configurações específicas para cada entidade, adicione-as aqui
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exemplo de configuração adicional
            modelBuilder.Entity<Documento>().ToTable("Documentos");
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios");
            modelBuilder.Entity<Permissao>().ToTable("Permissao");
            modelBuilder.Entity<Versaodocumento>().ToTable("Versaodocumento");

            // Configure relacionamentos e chaves primárias, se necessário
            modelBuilder.Entity<Documento>()
               .HasKey(d => d.id_documento); // Substitua 'Id' pelo nome da chave primária se diferente

            modelBuilder.Entity<UsuarioModel>()
               .HasKey(u => u.IdUsuarios); // Substitua 'IdUsuarios' pelo nome da chave primária se diferente
            
            modelBuilder.Entity<Permissao>()
               .HasKey(u => u.IdPermissao); // Substitua 'IdUsuarios' pelo nome da chave primária se diferente

            modelBuilder.Entity<Versaodocumento>()
               .HasKey(u => u.IdVersaodocumento); // Substitua 'IdUsuarios' pelo nome da chave primária se diferente    
        }
    }
}
