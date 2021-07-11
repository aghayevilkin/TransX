using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class UserVerificationUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "verificationicon",
                table: "AspNetUsers",
                newName: "IsVerify");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVerify",
                table: "AspNetUsers",
                newName: "verificationicon");
        }
    }
}
