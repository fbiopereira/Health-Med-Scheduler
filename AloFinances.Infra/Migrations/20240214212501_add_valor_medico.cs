using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloFinances.Infra.Migrations
{
    /// <inheritdoc />
    public partial class add_valor_medico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE medico SET Valor = 0 WHERE Valor = ''");
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "medico",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Valor",
                table: "medico",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
