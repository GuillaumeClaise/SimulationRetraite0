using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimulationRetraite0.Migrations.RetraiteSimulations
{
    /// <inheritdoc />
    public partial class AddTabDescriptionToTableau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tab_Description",
                schema: "dbo",
                table: "Tableaux",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tab_Description",
                schema: "dbo",
                table: "Tableaux");
        }
    }
}
