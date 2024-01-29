using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloDoutor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class agendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agendamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EspecialidadeMedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHoraAtendimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    StatusAgendamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agendamento_especialidadeMedico_EspecialidadeMedicoId",
                        column: x => x.EspecialidadeMedicoId,
                        principalTable: "especialidadeMedico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_agendamento_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_EspecialidadeMedicoId",
                table: "agendamento",
                column: "EspecialidadeMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_PacienteId",
                table: "agendamento",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");
        }
    }
}
