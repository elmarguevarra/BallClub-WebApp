using Microsoft.EntityFrameworkCore.Migrations;

namespace BallClub.Migrations
{
    public partial class AddPlayersToDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialMediaLinks",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMediaLinks",
                table: "Players");
        }
    }
}
