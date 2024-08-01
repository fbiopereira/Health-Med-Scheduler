using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMedScheduler.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class agendas_medicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agendas_medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    hora_fim = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendas_medico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agendas_medico_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendas_medico_MedicoId",
                table: "agendas_medico",
                column: "MedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendas_medico");
        }
    }
}
