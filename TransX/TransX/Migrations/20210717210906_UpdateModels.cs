using Microsoft.EntityFrameworkCore.Migrations;

namespace TransX.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Histories",
                newName: "SubTitle");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "AboutMissions",
                newName: "SubTitle");

            migrationBuilder.CreateTable(
                name: "AboutVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutVideos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutVideos");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "Histories",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "AboutMissions",
                newName: "Content");
        }
    }
}
