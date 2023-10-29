using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class EspacioDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EspacioId",
                table: "Usuarios",
                type: "int",
                nullable: true);

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
                    table.ForeignKey(
                        name: "FK_Espacios_Usuarios_AdminCorreo",
                        column: x => x.AdminCorreo,
                        principalTable: "Usuarios",
                        principalColumn: "Correo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cambios",
                columns: table => new
                {
                    FechaDeCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    Pesos = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambios", x => new { x.FechaDeCambio, x.Moneda });
                    table.ForeignKey(
                        name: "FK_Cambios_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.FechaCreacion);
                    table.ForeignKey(
                        name: "FK_Cuentas_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Titulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MontoMaximo = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Titulo);
                    table.ForeignKey(
                        name: "FK_Objetivos_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
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
                    table.PrimaryKey("PK_Categorias", x => x.Nombre);
                    table.ForeignKey(
                        name: "FK_Categorias_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categorias_Objetivos_ObjetivoTitulo",
                        column: x => x.ObjetivoTitulo,
                        principalTable: "Objetivos",
                        principalColumn: "Titulo");
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    IdTransaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    CuentaMonetariaFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaTransaccionNombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.IdTransaccion);
                    table.ForeignKey(
                        name: "FK_Transacciones_Categorias_CategoriaTransaccionNombre",
                        column: x => x.CategoriaTransaccionNombre,
                        principalTable: "Categorias",
                        principalColumn: "Nombre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuentas_CuentaMonetariaFechaCreacion",
                        column: x => x.CuentaMonetariaFechaCreacion,
                        principalTable: "Cuentas",
                        principalColumn: "FechaCreacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EspacioId",
                table: "Usuarios",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cambios_EspacioId",
                table: "Cambios",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EspacioId",
                table: "Categorias",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_ObjetivoTitulo",
                table: "Categorias",
                column: "ObjetivoTitulo");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_EspacioId",
                table: "Cuentas",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdminCorreo",
                table: "Espacios",
                column: "AdminCorreo");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_EspacioId",
                table: "Objetivos",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaTransaccionNombre",
                table: "Transacciones",
                column: "CategoriaTransaccionNombre");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaMonetariaFechaCreacion",
                table: "Transacciones",
                column: "CuentaMonetariaFechaCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_EspacioId",
                table: "Transacciones",
                column: "EspacioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Espacios_EspacioId",
                table: "Usuarios",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Espacios_EspacioId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cambios");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EspacioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EspacioId",
                table: "Usuarios");
        }
    }
}
