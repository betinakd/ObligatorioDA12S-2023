using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class EspacioUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspaciosUsuarios");
        }
    }
}
