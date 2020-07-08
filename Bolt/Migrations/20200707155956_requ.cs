using Microsoft.EntityFrameworkCore.Migrations;

namespace Bolt.Migrations
{
    public partial class requ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "SearchResults",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchEngine",
                table: "SearchResults",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "SearchResults",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "SearchEngine",
                table: "SearchResults",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
