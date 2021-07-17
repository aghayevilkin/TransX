using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class BlogUpdateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogStatus",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogStatus",
                table: "Blogs");
        }
    }
}
