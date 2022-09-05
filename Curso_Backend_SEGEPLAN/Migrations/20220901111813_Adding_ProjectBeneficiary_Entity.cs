using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso_Backend_SEGEPLAN.Migrations
{
    public partial class Adding_ProjectBeneficiary_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProyectosBeneficiarios",
                columns: table => new
                {
                    Proyecto_Id = table.Column<int>(type: "int", nullable: false),
                    Beneficiario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosBeneficiarios", x => new { x.Proyecto_Id, x.Beneficiario_Id });
                    table.ForeignKey(
                        name: "FK_ProyectosBeneficiarios_Beneficiarios_Beneficiario_Id",
                        column: x => x.Beneficiario_Id,
                        principalTable: "Beneficiarios",
                        principalColumn: "Beneficiario_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectosBeneficiarios_Proyectos_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyectos",
                        principalColumn: "Proyecto_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosBeneficiarios_Beneficiario_Id",
                table: "ProyectosBeneficiarios",
                column: "Beneficiario_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProyectosBeneficiarios");
        }
    }
}
