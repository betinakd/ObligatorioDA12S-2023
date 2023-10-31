using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class pk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Espacios_EspacioId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivoCategoria_Categorias_CategoriasNombre",
                table: "ObjetivoCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo",
                table: "ObjetivoCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Objetivos_Espacios_EspacioId",
                table: "Objetivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Categorias_CategoriaTransaccionNombre",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_CategoriaTransaccionNombre",
                table: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Objetivos",
                table: "Objetivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjetivoCategoria",
                table: "ObjetivoCategoria");

            migrationBuilder.DropIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo",
                table: "ObjetivoCategoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_EspacioId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Objetivos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categorias");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaTransaccionEspacioId",
                table: "Transacciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Objetivos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasEspacioId",
                table: "ObjetivoCategoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjetivosEspacioId",
                table: "ObjetivoCategoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objetivos",
                table: "Objetivos",
                columns: new[] { "Titulo", "EspacioId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjetivoCategoria",
                table: "ObjetivoCategoria",
                columns: new[] { "CategoriasEspacioId", "CategoriasNombre", "ObjetivosTitulo", "ObjetivosEspacioId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                columns: new[] { "EspacioId", "Nombre" });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                table: "Transacciones",
                columns: new[] { "CategoriaTransaccionEspacioId", "CategoriaTransaccionNombre" });

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo_ObjetivosEspacioId",
                table: "ObjetivoCategoria",
                columns: new[] { "ObjetivosTitulo", "ObjetivosEspacioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Espacios_EspacioId",
                table: "Categorias",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivoCategoria_Categorias_CategoriasEspacioId_CategoriasNombre",
                table: "ObjetivoCategoria",
                columns: new[] { "CategoriasEspacioId", "CategoriasNombre" },
                principalTable: "Categorias",
                principalColumns: new[] { "EspacioId", "Nombre" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo_ObjetivosEspacioId",
                table: "ObjetivoCategoria",
                columns: new[] { "ObjetivosTitulo", "ObjetivosEspacioId" },
                principalTable: "Objetivos",
                principalColumns: new[] { "Titulo", "EspacioId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objetivos_Espacios_EspacioId",
                table: "Objetivos",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Categorias_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                table: "Transacciones",
                columns: new[] { "CategoriaTransaccionEspacioId", "CategoriaTransaccionNombre" },
                principalTable: "Categorias",
                principalColumns: new[] { "EspacioId", "Nombre" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Espacios_EspacioId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivoCategoria_Categorias_CategoriasEspacioId_CategoriasNombre",
                table: "ObjetivoCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo_ObjetivosEspacioId",
                table: "ObjetivoCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Objetivos_Espacios_EspacioId",
                table: "Objetivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Categorias_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_CategoriaTransaccionEspacioId_CategoriaTransaccionNombre",
                table: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Objetivos",
                table: "Objetivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjetivoCategoria",
                table: "ObjetivoCategoria");

            migrationBuilder.DropIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo_ObjetivosEspacioId",
                table: "ObjetivoCategoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "CategoriaTransaccionEspacioId",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "CategoriasEspacioId",
                table: "ObjetivoCategoria");

            migrationBuilder.DropColumn(
                name: "ObjetivosEspacioId",
                table: "ObjetivoCategoria");

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Objetivos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Objetivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Categorias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objetivos",
                table: "Objetivos",
                column: "Titulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjetivoCategoria",
                table: "ObjetivoCategoria",
                columns: new[] { "CategoriasNombre", "ObjetivosTitulo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CategoriaTransaccionNombre",
                table: "Transacciones",
                column: "CategoriaTransaccionNombre");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo",
                table: "ObjetivoCategoria",
                column: "ObjetivosTitulo");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EspacioId",
                table: "Categorias",
                column: "EspacioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Espacios_EspacioId",
                table: "Categorias",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivoCategoria_Categorias_CategoriasNombre",
                table: "ObjetivoCategoria",
                column: "CategoriasNombre",
                principalTable: "Categorias",
                principalColumn: "Nombre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo",
                table: "ObjetivoCategoria",
                column: "ObjetivosTitulo",
                principalTable: "Objetivos",
                principalColumn: "Titulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objetivos_Espacios_EspacioId",
                table: "Objetivos",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Categorias_CategoriaTransaccionNombre",
                table: "Transacciones",
                column: "CategoriaTransaccionNombre",
                principalTable: "Categorias",
                principalColumn: "Nombre",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
