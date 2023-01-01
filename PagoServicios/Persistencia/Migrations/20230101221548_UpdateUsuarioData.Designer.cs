﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistencia;

#nullable disable

namespace Persistencia.Migrations
{
    [DbContext(typeof(ApiDBContext))]
    [Migration("20230101221548_UpdateUsuarioData")]
    partial class UpdateUsuarioData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dominio.CuentaPagar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Importe")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("numeric");

                    b.Property<int>("ServicioID")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("integer");

                    b.Property<decimal?>("cuota")
                        .HasColumnType("numeric");

                    b.HasKey("ID");

                    b.HasIndex("ServicioID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("CuentaPagar");
                });

            modelBuilder.Entity("Dominio.PagoRealizado", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CuentaPagarID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Importe")
                        .HasColumnType("numeric");

                    b.Property<string>("Observacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CuentaPagarID");

                    b.ToTable("PagoRealizado");
                });

            modelBuilder.Entity("Dominio.Servicios", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("Dominio.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("NIS")
                        .HasColumnType("text");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("numero_cedula")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Dominio.CuentaPagar", b =>
                {
                    b.HasOne("Dominio.Servicios", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Dominio.PagoRealizado", b =>
                {
                    b.HasOne("Dominio.CuentaPagar", "CuentaPagar")
                        .WithMany()
                        .HasForeignKey("CuentaPagarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CuentaPagar");
                });
#pragma warning restore 612, 618
        }
    }
}