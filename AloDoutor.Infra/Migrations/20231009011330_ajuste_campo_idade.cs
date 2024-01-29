using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AloDoutor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_campo_idade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "idade",
                table: "paciente",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "idade",
                table: "paciente",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldMaxLength: 3);
        }
    }
}
