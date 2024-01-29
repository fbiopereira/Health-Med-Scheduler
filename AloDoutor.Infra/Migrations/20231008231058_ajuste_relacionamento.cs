using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloDoutor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_relacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_especialidadeMedico_medico_EspecialidadeId",
                table: "especialidadeMedico");

            migrationBuilder.CreateIndex(
                name: "IX_especialidadeMedico_MedicoId",
                table: "especialidadeMedico",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_especialidadeMedico_medico_MedicoId",
                table: "especialidadeMedico",
                column: "MedicoId",
                principalTable: "medico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_especialidadeMedico_medico_MedicoId",
                table: "especialidadeMedico");

            migrationBuilder.DropIndex(
                name: "IX_especialidadeMedico_MedicoId",
                table: "especialidadeMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_especialidadeMedico_medico_EspecialidadeId",
                table: "especialidadeMedico",
                column: "EspecialidadeId",
                principalTable: "medico",
                principalColumn: "Id");
        }
    }
}
