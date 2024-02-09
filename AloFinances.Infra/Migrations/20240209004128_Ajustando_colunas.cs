using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloFinances.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ajustando_colunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    data_Criacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
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
                    nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cep = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    endereco = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    data_Criacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medico");

            migrationBuilder.DropTable(
                name: "paciente");
        }
    }
}
