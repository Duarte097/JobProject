﻿// <auto-generated />
using System;
using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiCrud.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ApiCrud.Api.Models.Versaodocumento", b =>
                {
                    b.Property<int>("IdVersaodocumento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdVersaodocumento"));

                    b.Property<string>("Caminhoversaoarquivo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("Dataversao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<string>("Numeroversao")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdVersaodocumento");

                    b.HasIndex("DocumentoId");

                    b.ToTable("Versaodocumento", (string)null);
                });

            modelBuilder.Entity("ApiCrud.Models.Documento", b =>
                {
                    b.Property<int>("IdDocumento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdDocumento"));

                    b.Property<string>("Caminho")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Categoria")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataUpload")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("VersaoAtual")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdDocumento");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Documentos", (string)null);
                });

            modelBuilder.Entity("ApiCrud.Models.Permissao", b =>
                {
                    b.Property<int>("IdPermissao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPermissao"));

                    b.Property<int>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<string>("Permissaotipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("IdPermissao");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Permissao", (string)null);
                });

            modelBuilder.Entity("ApiCrud.Models.UsuarioModel", b =>
                {
                    b.Property<int>("IdUsuarios")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdUsuarios"));

                    b.Property<DateTime?>("Datacriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Papel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdUsuarios");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("ApiCrud.Api.Models.Versaodocumento", b =>
                {
                    b.HasOne("ApiCrud.Models.Documento", "Documento")
                        .WithMany("Versaodocumento")
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Documento");
                });

            modelBuilder.Entity("ApiCrud.Models.Documento", b =>
                {
                    b.HasOne("ApiCrud.Models.UsuarioModel", "Usuario")
                        .WithMany("Documentos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiCrud.Models.Permissao", b =>
                {
                    b.HasOne("ApiCrud.Models.Documento", "Documento")
                        .WithMany("Permissoes")
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiCrud.Models.UsuarioModel", "Usuario")
                        .WithMany("Permissoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Documento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiCrud.Models.Documento", b =>
                {
                    b.Navigation("Permissoes");

                    b.Navigation("Versaodocumento");
                });

            modelBuilder.Entity("ApiCrud.Models.UsuarioModel", b =>
                {
                    b.Navigation("Documentos");

                    b.Navigation("Permissoes");
                });
#pragma warning restore 612, 618
        }
    }
}
