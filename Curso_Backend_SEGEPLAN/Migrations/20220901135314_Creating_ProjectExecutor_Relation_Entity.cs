using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso_Backend_SEGEPLAN.Migrations
{
    public partial class Creating_ProjectExecutor_Relation_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProyectosEjecutores",
                columns: table => new
                {
                    Proyecto_Id = table.Column<int>(type: "int", nullable: false),
                    Ejecutor_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosEjecutores", x => new { x.Proyecto_Id, x.Ejecutor_Id });
                    table.ForeignKey(
                        name: "FK_ProyectosEjecutores_Ejecutores_Ejecutor_Id",
                        column: x => x.Ejecutor_Id,
                        principalTable: "Ejecutores",
                        principalColumn: "Ejecutor_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectosEjecutores_Proyectos_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyectos",
                        principalColumn: "Proyecto_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosEjecutores_Ejecutor_Id",
                table: "ProyectosEjecutores",
                column: "Ejecutor_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProyectosEjecutores");
        }
    }
}
