using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class add_price_course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_price",
                table: "courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "price",
                table: "courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_price",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "price",
                table: "courses");
        }
    }
}
