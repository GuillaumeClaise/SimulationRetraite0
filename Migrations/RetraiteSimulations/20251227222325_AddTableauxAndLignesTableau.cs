using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimulationRetraite0.Migrations.RetraiteSimulations
{
    /// <inheritdoc />
    public partial class AddTableauxAndLignesTableau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tableaux",
                schema: "dbo",
                columns: table => new
                {
                    Tab_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tab_TdId = table.Column<int>(type: "int", nullable: false),
                    Tab_NumeroTableau = table.Column<int>(type: "int", nullable: false),
                    Tab_NomTableau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tab_DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tableaux", x => x.Tab_Id);
                    table.ForeignKey(
                        name: "FK_Tableaux_Dossiers_Tab_TdId",
                        column: x => x.Tab_TdId,
                        principalSchema: "dbo",
                        principalTable: "Dossiers",
                        principalColumn: "Td_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LignesTableau",
                schema: "dbo",
                columns: table => new
                {
                    Lt_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lt_TabId = table.Column<int>(type: "int", nullable: false),
                    Lt_NumeroLigne = table.Column<int>(type: "int", nullable: false),
                    Lt_Libelle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Lt_Valeur1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur5 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur6 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur7 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur8 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur9 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lt_Valeur10 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesTableau", x => x.Lt_Id);
                    table.ForeignKey(
                        name: "FK_LignesTableau_Tableaux_Lt_TabId",
                        column: x => x.Lt_TabId,
                        principalSchema: "dbo",
                        principalTable: "Tableaux",
                        principalColumn: "Tab_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LignesTableau_Lt_TabId",
                schema: "dbo",
                table: "LignesTableau",
                column: "Lt_TabId");

            migrationBuilder.CreateIndex(
                name: "IX_Tableaux_Tab_TdId",
                schema: "dbo",
                table: "Tableaux",
                column: "Tab_TdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignesTableau",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tableaux",
                schema: "dbo");
        }
    }
}
