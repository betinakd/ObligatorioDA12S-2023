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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    Pesos = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoActivo = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true),
                    ObjetivoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Espacios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objetivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoMaximo = table.Column<double>(type: "float", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivo", x => x.Id);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    CuentaMonetariaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaTransaccionId = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaccion_Categoria_CategoriaTransaccionId",
                        column: x => x.CategoriaTransaccionId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuenta_CuentaMonetariaId",
                        column: x => x.CuentaMonetariaId,
                        principalTable: "Cuenta",
                        principalColumn: "Id",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEspacioPrincipal = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
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
                name: "IX_Categoria_ObjetivoId",
                table: "Categoria",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_EspacioId",
                table: "Cuenta",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdminId",
                table: "Espacios",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivo_EspacioId",
                table: "Objetivo",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CategoriaTransaccionId",
                table: "Transaccion",
                column: "CategoriaTransaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CuentaMonetariaId",
                table: "Transaccion",
                column: "CuentaMonetariaId");

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
                name: "FK_Categoria_Objetivo_ObjetivoId",
                table: "Categoria",
                column: "ObjetivoId",
                principalTable: "Objetivo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Espacios_EspacioId",
                table: "Cuenta",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Espacios_Usuarios_AdminId",
                table: "Espacios",
                column: "AdminId",
                principalTable: "Usuarios",
                principalColumn: "Id",
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
