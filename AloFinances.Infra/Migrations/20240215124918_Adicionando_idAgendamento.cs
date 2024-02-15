using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloFinances.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_idAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgendamentoId",
                table: "contas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgendamentoId",
                table: "contas");
        }
    }
}
