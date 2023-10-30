﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(UsuariosDbContext))]
    partial class UsuariosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaObjetivo", b =>
                {
                    b.Property<string>("CategoriasNombre")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ObjetivosTitulo")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoriasNombre", "ObjetivosTitulo");

                    b.HasIndex("ObjetivosTitulo");

                    b.ToTable("ObjetivoCategoria", (string)null);
                });

            modelBuilder.Entity("Domain.Cambio", b =>
                {
                    b.Property<DateTime>("FechaDeCambio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Moneda")
                        .HasColumnType("int");

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<double>("Pesos")
                        .HasColumnType("float");

                    b.HasKey("FechaDeCambio", "Moneda", "EspacioId");

                    b.HasIndex("EspacioId");

                    b.ToTable("Cambios");
                });

            modelBuilder.Entity("Domain.Categoria", b =>
                {
                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EspacioId")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Nombre");

                    b.HasIndex("EspacioId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Domain.Cuenta", b =>
                {
                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EspacioId")
                        .HasColumnType("int");

                    b.Property<int>("Moneda")
                        .HasColumnType("int");

                    b.HasKey("FechaCreacion");

                    b.HasIndex("EspacioId");

                    b.ToTable("Cuentas");
                });

            modelBuilder.Entity("Domain.Espacio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminCorreo");

                    b.ToTable("Espacios");
                });

            modelBuilder.Entity("Domain.EspacioUsuario", b =>
                {
                    b.Property<int>("IdEspacio")
                        .HasColumnType("int");

                    b.Property<string>("CorreoUsuario")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdEspacio", "CorreoUsuario");

                    b.HasIndex("CorreoUsuario");

                    b.ToTable("EspaciosUsuarios");
                });

            modelBuilder.Entity("Domain.Objetivo", b =>
                {
                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EspacioId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<double>("MontoMaximo")
                        .HasColumnType("float");

                    b.HasKey("Titulo");

                    b.HasIndex("EspacioId");

                    b.ToTable("Objetivos");
                });

            modelBuilder.Entity("Domain.Transaccion", b =>
                {
                    b.Property<int>("IdTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"));

                    b.Property<string>("CategoriaTransaccionNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CuentaMonetariaFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EspacioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaTransaccion")
                        .HasColumnType("datetime2");

                    b.Property<int>("Moneda")
                        .HasColumnType("int");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTransaccion");

                    b.HasIndex("CategoriaTransaccionNombre");

                    b.HasIndex("CuentaMonetariaFechaCreacion");

                    b.HasIndex("EspacioId");

                    b.ToTable("Transacciones");
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEspacioPrincipal")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Correo");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CategoriaObjetivo", b =>
                {
                    b.HasOne("Domain.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Objetivo", null)
                        .WithMany()
                        .HasForeignKey("ObjetivosTitulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Cambio", b =>
                {
                    b.HasOne("Domain.Espacio", null)
                        .WithMany("Cambios")
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Categoria", b =>
                {
                    b.HasOne("Domain.Espacio", null)
                        .WithMany("Categorias")
                        .HasForeignKey("EspacioId");
                });

            modelBuilder.Entity("Domain.Cuenta", b =>
                {
                    b.HasOne("Domain.Espacio", null)
                        .WithMany("Cuentas")
                        .HasForeignKey("EspacioId");
                });

            modelBuilder.Entity("Domain.Espacio", b =>
                {
                    b.HasOne("Domain.Usuario", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminCorreo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Domain.EspacioUsuario", b =>
                {
                    b.HasOne("Domain.Usuario", "Usuario")
                        .WithMany("EspaciosUsuarios")
                        .HasForeignKey("CorreoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Espacio", "Espacio")
                        .WithMany("UsuariosInvitados")
                        .HasForeignKey("IdEspacio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espacio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Objetivo", b =>
                {
                    b.HasOne("Domain.Espacio", null)
                        .WithMany("Objetivos")
                        .HasForeignKey("EspacioId");
                });

            modelBuilder.Entity("Domain.Transaccion", b =>
                {
                    b.HasOne("Domain.Categoria", "CategoriaTransaccion")
                        .WithMany()
                        .HasForeignKey("CategoriaTransaccionNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Cuenta", "CuentaMonetaria")
                        .WithMany()
                        .HasForeignKey("CuentaMonetariaFechaCreacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Espacio", null)
                        .WithMany("Transacciones")
                        .HasForeignKey("EspacioId");

                    b.Navigation("CategoriaTransaccion");

                    b.Navigation("CuentaMonetaria");
                });

            modelBuilder.Entity("Domain.Espacio", b =>
                {
                    b.Navigation("Cambios");

                    b.Navigation("Categorias");

                    b.Navigation("Cuentas");

                    b.Navigation("Objetivos");

                    b.Navigation("Transacciones");

                    b.Navigation("UsuariosInvitados");
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Navigation("EspaciosUsuarios");
                });
#pragma warning restore 612, 618
        }
    }
}