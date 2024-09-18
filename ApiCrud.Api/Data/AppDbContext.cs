using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data;

public class AppDbContext : DbContext
{
   public DbSet<Documento> Documentos {get; set;}

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
        optionsBuilder.UseMySql("server=localhost;database=digitaldoc;user=root;password=1234", new MySqlServerVersion(new Version(8, 0, 26)));
        base.OnConfiguring(optionsBuilder);
   }
}