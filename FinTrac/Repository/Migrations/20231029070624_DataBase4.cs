using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class DataBase4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cambios_Espacios_EspacioId",
                table: "Cambios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cambios",
                table: "Cambios");

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Cambios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cambios",
                table: "Cambios",
                columns: new[] { "FechaDeCambio", "Moneda", "EspacioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cambios_Espacios_EspacioId",
                table: "Cambios",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cambios_Espacios_EspacioId",
                table: "Cambios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cambios",
                table: "Cambios");

            migrationBuilder.AlterColumn<int>(
                name: "EspacioId",
                table: "Cambios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cambios",
                table: "Cambios",
                columns: new[] { "FechaDeCambio", "Moneda" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cambios_Espacios_EspacioId",
                table: "Cambios",
                column: "EspacioId",
                principalTable: "Espacios",
                principalColumn: "Id");
        }
    }
}
