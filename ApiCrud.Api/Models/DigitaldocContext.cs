using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;


/*namespace ApiCrud.Api.Models;

public partial class DigitaldocContext : DbContext
{
    public DigitaldocContext()
    {
    }

    public DigitaldocContext(DbContextOptions<DigitaldocContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Permisao> Permisaos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Versaodocumento> Versaodocumentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=digitaldoc;user=root;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PRIMARY");

            entity.ToTable("documento");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.IdDocumento).HasColumnName("id_documento");
            entity.Property(e => e.Caminhorarquivo)
                .HasMaxLength(500)
                .HasColumnName("caminhorarquivo");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.Dataupload)
                .HasColumnType("datetime")
                .HasColumnName("dataupload");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Versaoatual)
                .HasMaxLength(10)
                .HasColumnName("versaoatual");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documento_ibfk_1");
        });

        modelBuilder.Entity<Permisao>(entity =>
        {
            entity.HasKey(e => e.IdPermisao).HasName("PRIMARY");

            entity.ToTable("permisao");

            entity.HasIndex(e => e.DocumentoId, "documento_id");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.IdPermisao).HasColumnName("id_permisao");
            entity.Property(e => e.DocumentoId).HasColumnName("documento_id");
            entity.Property(e => e.Permissaotipo)
                .HasMaxLength(50)
                .HasColumnName("permissaotipo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Documento).WithMany(p => p.Permisaos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permisao_ibfk_2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Permisaos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permisao_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuarios).HasColumnName("id_usuarios");
            entity.Property(e => e.Datacriacao)
                .HasColumnType("datetime")
                .HasColumnName("datacriacao");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Papel)
                .HasMaxLength(50)
                .HasColumnName("papel");
            entity.Property(e => e.SenhaHash)
                .HasMaxLength(255)
                .HasColumnName("senhaHash");
        });

        modelBuilder.Entity<Versaodocumento>(entity =>
        {
            entity.HasKey(e => e.IdVersaodocumento).HasName("PRIMARY");

            entity.ToTable("versaodocumento");

            entity.HasIndex(e => e.DocumentoId, "documento_id");

            entity.Property(e => e.IdVersaodocumento).HasColumnName("id_versaodocumento");
            entity.Property(e => e.Caminhoversaoarquivo)
                .HasMaxLength(500)
                .HasColumnName("caminhoversaoarquivo");
            entity.Property(e => e.Dataversao)
                .HasColumnType("datetime")
                .HasColumnName("dataversao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.DocumentoId).HasColumnName("documento_id");
            entity.Property(e => e.Numeroversao)
                .HasMaxLength(10)
                .HasColumnName("numeroversao");

            entity.HasOne(d => d.Documento).WithMany(p => p.Versaodocumentos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("versaodocumento_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}*/
