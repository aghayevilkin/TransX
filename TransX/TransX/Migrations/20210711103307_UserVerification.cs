using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class UserVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "verificationicon",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verificationicon",
                table: "AspNetUsers");
        }
    }
}
