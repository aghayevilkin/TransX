using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class CustomUserAddTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTeam",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTeam",
                table: "AspNetUsers");
        }
    }
}
