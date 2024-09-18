﻿// <auto-generated />
using System;
using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiCrud.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240918202307_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ApiCrud.Models.Documento", b =>
                {
                    b.Property<int>("id_documento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id_documento"));

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataUpload")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModelIdUsuarios")
                        .HasColumnType("int");

                    b.Property<string>("Versao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id_documento");

                    b.HasIndex("UsuarioModelIdUsuarios");

                    b.ToTable("Documentos", (string)null);
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
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Papel")
                        .HasColumnType("longtext");

                    b.Property<string>("SenhaHash")
                        .HasColumnType("longtext");

                    b.HasKey("IdUsuarios");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("ApiCrud.Models.Documento", b =>
                {
                    b.HasOne("ApiCrud.Models.UsuarioModel", null)
                        .WithMany("Documentos")
                        .HasForeignKey("UsuarioModelIdUsuarios");
                });

            modelBuilder.Entity("ApiCrud.Models.UsuarioModel", b =>
                {
                    b.Navigation("Documentos");
                });
#pragma warning restore 612, 618
        }
    }
}
