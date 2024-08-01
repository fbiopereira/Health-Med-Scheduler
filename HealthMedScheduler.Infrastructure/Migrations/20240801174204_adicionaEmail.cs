using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMedScheduler.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionaEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "paciente",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "medico",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "email",
                table: "medico");
        }
    }
}
