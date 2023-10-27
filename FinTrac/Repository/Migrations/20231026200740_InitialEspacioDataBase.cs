using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialEspacioDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cambio",
                columns: table => new
                {
                    FechaDeCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    Pesos = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambio", x => x.FechaDeCambio);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstadoActivo = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true),
                    ObjetivoTitulo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.FechaCreacion);
                });

            migrationBuilder.CreateTable(
                name: "Espacios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminCorreo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objetivo",
                columns: table => new
                {
                    Titulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MontoMaximo = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivo", x => x.Titulo);
                    table.ForeignKey(
                        name: "FK_Objetivo_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    IdTransaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    CuentaMonetariaFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaTransaccionNombre = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.IdTransaccion);
                    table.ForeignKey(
                        name: "FK_Transaccion_Categoria_CategoriaTransaccionNombre",
                        column: x => x.CategoriaTransaccionNombre,
                        principalTable: "Categoria",
                        principalColumn: "Nombre");
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuenta_CuentaMonetariaFechaCreacion",
                        column: x => x.CuentaMonetariaFechaCreacion,
                        principalTable: "Cuenta",
                        principalColumn: "FechaCreacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Correo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdEspacioPrincipal = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Correo);
                    table.ForeignKey(
                        name: "FK_Usuarios_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_EspacioId",
                table: "Cambio",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_EspacioId",
                table: "Categoria",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_ObjetivoTitulo",
                table: "Categoria",
                column: "ObjetivoTitulo");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_EspacioId",
                table: "Cuenta",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdminCorreo",
                table: "Espacios",
                column: "AdminCorreo");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivo_EspacioId",
                table: "Objetivo",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CategoriaTransaccionNombre",
                table: "Transaccion",
                column: "CategoriaTransaccionNombre");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CuentaMonetariaFechaCreacion",
                table: "Transaccion",
                column: "CuentaMonetariaFechaCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_EspacioId",
                table: "Transaccion",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EspacioId",
                table: "Usuarios",
                column: "EspacioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cambio_Espacios_EspacioId",
                table: "Cambio",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Espacios_EspacioId",
                table: "Categoria",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Objetivo_ObjetivoTitulo",
                table: "Categoria",
                column: "ObjetivoTitulo",
                principalTable: "Objetivo",
                principalColumn: "Titulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Espacios_EspacioId",
                table: "Cuenta",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Espacios_Usuarios_AdminCorreo",
                table: "Espacios",
                column: "AdminCorreo",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Espacios_EspacioId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cambio");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Objetivo");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
