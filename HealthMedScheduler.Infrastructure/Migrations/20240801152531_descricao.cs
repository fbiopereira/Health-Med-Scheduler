using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMedScheduler.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class descricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especialidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    crm = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cep = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    endereco = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idade = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cep = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    endereco = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "especialidadeMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EspecialidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dataRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidadeMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_especialidadeMedico_especialidade_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "especialidade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_especialidadeMedico_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_especialidadeMedico_EspecialidadeId",
                table: "especialidadeMedico",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_especialidadeMedico_MedicoId",
                table: "especialidadeMedico",
                column: "MedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");

            migrationBuilder.DropTable(
                name: "especialidadeMedico");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "especialidade");

            migrationBuilder.DropTable(
                name: "medico");
        }
    }
}
