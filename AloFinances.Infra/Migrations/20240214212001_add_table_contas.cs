using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloFinances.Infra.Migrations
{
    /// <inheritdoc />
    public partial class add_table_contas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia",
                startValue: 1000L);

            migrationBuilder.AddColumn<string>(
                name: "Valor",
                table: "medico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusConta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contas_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_contas_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "preco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preco", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contas_MedicoId",
                table: "contas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_contas_PacienteId",
                table: "contas",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contas");

            migrationBuilder.DropTable(
                name: "preco");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "medico");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
