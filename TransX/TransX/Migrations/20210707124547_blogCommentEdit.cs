using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class blogCommentEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "BlogComments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BlogComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_AspNetUsers_UserId",
                table: "BlogComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_AspNetUsers_UserId",
                table: "BlogComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogComments");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BlogComments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "BlogComments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
