using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasoPractico1.Migrations
{
    /// <inheritdoc />
    public partial class QuitarID_Horario_Parada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rutaId",
                table: "Paradas");

            migrationBuilder.DropColumn(
                name: "rutaId",
                table: "Horarios");

            migrationBuilder.RenameColumn(
                name: "horafil",
                table: "Horarios",
                newName: "horafin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "horafin",
                table: "Horarios",
                newName: "horafil");

            migrationBuilder.AddColumn<int>(
                name: "rutaId",
                table: "Paradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rutaId",
                table: "Horarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
