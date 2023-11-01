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
                name: "Usuarios",
                columns: table => new
                {
                    Correo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdEspacioPrincipal = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Correo);
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
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    Pesos = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambios", x => new { x.FechaDeCambio, x.Moneda, x.EspacioId });
                    table.ForeignKey(
                        name: "FK_Cambios_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstadoActivo = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => new { x.EspacioId, x.Nombre });
                    table.ForeignKey(
                        name: "FK_Categorias_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true),
                    Tipo_cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BancoEmisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTarjeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditoDisponible = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EspaciosUsuarios",
                columns: table => new
                {
                    IdEspacio = table.Column<int>(type: "int", nullable: false),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspaciosUsuarios", x => new { x.IdEspacio, x.CorreoUsuario });
                    table.ForeignKey(
                        name: "FK_EspaciosUsuarios_Espacios_IdEspacio",
                        column: x => x.IdEspacio,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EspaciosUsuarios_Usuarios_CorreoUsuario",
                        column: x => x.CorreoUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Correo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    EspacioId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MontoMaximo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => new { x.Titulo, x.EspacioId });
                    table.ForeignKey(
                        name: "FK_Objetivos_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CuentaMonetariaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaTransaccionEspacioId = table.Column<int>(type: "int", nullable: false),
                    CategoriaTransaccionNombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EspacioId = table.Column<int>(type: "int", nullable: true),
                    Tipo_Transaccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.IdTransaccion);
                    table.ForeignKey(
                        name: "FK_Transacciones_Categorias_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                        columns: x => new { x.CategoriaTransaccionEspacioId, x.CategoriaTransaccionNombre },
                        principalTable: "Categorias",
                        principalColumns: new[] { "EspacioId", "Nombre" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuentas_CuentaMonetariaId",
                        column: x => x.CuentaMonetariaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Espacios_EspacioId",
                        column: x => x.EspacioId,
                        principalTable: "Espacios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ObjetivoCategoria",
                columns: table => new
                {
                    CategoriasEspacioId = table.Column<int>(type: "int", nullable: false),
                    CategoriasNombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ObjetivosTitulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ObjetivosEspacioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivoCategoria", x => new { x.CategoriasEspacioId, x.CategoriasNombre, x.ObjetivosTitulo, x.ObjetivosEspacioId });
                    table.ForeignKey(
                        name: "FK_ObjetivoCategoria_Categorias_CategoriasEspacioId_CategoriasNombre",
                        columns: x => new { x.CategoriasEspacioId, x.CategoriasNombre },
                        principalTable: "Categorias",
                        principalColumns: new[] { "EspacioId", "Nombre" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo_ObjetivosEspacioId",
                        columns: x => new { x.ObjetivosTitulo, x.ObjetivosEspacioId },
                        principalTable: "Objetivos",
                        principalColumns: new[] { "Titulo", "EspacioId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cambios_EspacioId",
                table: "Cambios",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_EspacioId",
                table: "Cuentas",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_AdminCorreo",
                table: "Espacios",
                column: "AdminCorreo");

            migrationBuilder.CreateIndex(
                name: "IX_EspaciosUsuarios_CorreoUsuario",
                table: "EspaciosUsuarios",
                column: "CorreoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo_ObjetivosEspacioId",
                table: "ObjetivoCategoria",
                columns: new[] { "ObjetivosTitulo", "ObjetivosEspacioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_EspacioId",
                table: "Objetivos",
                column: "EspacioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                table: "Transacciones",
                columns: new[] { "CategoriaTransaccionEspacioId", "CategoriaTransaccionNombre" });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaMonetariaId",
                table: "Transacciones",
                column: "CuentaMonetariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_EspacioId",
                table: "Transacciones",
                column: "EspacioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cambios");

            migrationBuilder.DropTable(
                name: "EspaciosUsuarios");

            migrationBuilder.DropTable(
                name: "ObjetivoCategoria");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
