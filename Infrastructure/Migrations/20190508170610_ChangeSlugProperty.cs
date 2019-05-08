using Microsoft.EntityFrameworkCore.Migrations;

namespace Conduit.Infrastructure.Migrations
{
    public partial class ChangeSlugProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
