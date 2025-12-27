using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimulationRetraite0.Migrations.RetraiteSimulations
{
    /// <inheritdoc />
    public partial class AddDossiersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Dossiers",
                schema: "dbo",
                columns: table => new
                {
                    Td_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Td_NomClient = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Td_PrenomClient = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Td_DateDossier = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Td_NomDossier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Td_Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.Td_Id);
                });

            migrationBuilder.CreateTable(
                name: "TClients",
                schema: "dbo",
                columns: table => new
                {
                    TC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TC_Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TC_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TC_NomDossier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TClients", x => x.TC_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dossiers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TClients",
                schema: "dbo");
        }
    }
}
