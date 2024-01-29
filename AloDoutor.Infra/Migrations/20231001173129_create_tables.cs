using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloDoutor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class create_tables : Migration
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
                    descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
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
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    cep = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    crm = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    telefone = table.Column<int>(type: "int", maxLength: 11, nullable: false)
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
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    cep = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    idade = table.Column<int>(type: "int", nullable: false),
                    telefone = table.Column<int>(type: "int", maxLength: 11, nullable: false)
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
                        name: "FK_especialidadeMedico_medico_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "medico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_especialidadeMedico_EspecialidadeId",
                table: "especialidadeMedico",
                column: "EspecialidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
