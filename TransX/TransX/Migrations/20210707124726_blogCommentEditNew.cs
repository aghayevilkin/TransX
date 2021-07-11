using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class blogCommentEditNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BlogComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BlogComments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
