using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CategoriasObjetivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Objetivos_ObjetivoTitulo",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_ObjetivoTitulo",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ObjetivoTitulo",
                table: "Categorias");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Objetivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ObjetivoCategoria",
                columns: table => new
                {
                    CategoriasNombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ObjetivosTitulo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivoCategoria", x => new { x.CategoriasNombre, x.ObjetivosTitulo });
                    table.ForeignKey(
                        name: "FK_ObjetivoCategoria_Categorias_CategoriasNombre",
                        column: x => x.CategoriasNombre,
                        principalTable: "Categorias",
                        principalColumn: "Nombre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjetivoCategoria_Objetivos_ObjetivosTitulo",
                        column: x => x.ObjetivosTitulo,
                        principalTable: "Objetivos",
                        principalColumn: "Titulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivoCategoria_ObjetivosTitulo",
                table: "ObjetivoCategoria",
                column: "ObjetivosTitulo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjetivoCategoria");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Objetivos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categorias");

            migrationBuilder.AddColumn<string>(
                name: "ObjetivoTitulo",
                table: "Categorias",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_ObjetivoTitulo",
                table: "Categorias",
                column: "ObjetivoTitulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Objetivos_ObjetivoTitulo",
                table: "Categorias",
                column: "ObjetivoTitulo",
                principalTable: "Objetivos",
                principalColumn: "Titulo");
        }
    }
}
