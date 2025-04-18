﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SistemaDeUsuarios.Context;

#nullable disable

namespace SistemaDeUsuarios.Migrations
{
    [DbContext(typeof(SistemaDbContext))]
    [Migration("20250408184333_InitialMigrationsDbSistema")]
    partial class InitialMigrationsDbSistema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "prioridad", new[] { "baja", "intermedia", "alta" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SistemaDeUsuarios.Models.Folder", b =>
                {
                    b.Property<Guid>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("FolderId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("SistemaDeUsuarios.Models.Tareas", b =>
                {
                    b.Property<Guid>("NotaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime?>("FechaDeCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FolderId")
                        .HasColumnType("uuid");

                    b.Property<int>("PrioridadDeTarea")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("NotaId");

                    b.HasIndex("FolderId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("SistemaDeUsuarios.Models.Tareas", b =>
                {
                    b.HasOne("SistemaDeUsuarios.Models.Folder", "Folder")
                        .WithMany("Tareas")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FolderId");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("SistemaDeUsuarios.Models.Folder", b =>
                {
                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
