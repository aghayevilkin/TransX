using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class AddFaqModelUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Page",
                table: "Faqs");

            migrationBuilder.AddColumn<bool>(
                name: "IsQuestions",
                table: "Faqs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsQuestions",
                table: "Faqs");

            migrationBuilder.AddColumn<string>(
                name: "Page",
                table: "Faqs",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
