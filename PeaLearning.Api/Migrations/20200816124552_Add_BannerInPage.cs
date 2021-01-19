using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class Add_BannerInPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "banner_in_page",
                table: "banners",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banner_in_page",
                table: "banners");
        }
    }
}
