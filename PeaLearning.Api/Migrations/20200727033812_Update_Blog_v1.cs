using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class Update_Blog_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "blogs",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "home_flag",
                table: "blogs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hot_flag",
                table: "blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "blogs",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "seo_alias",
                table: "blogs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seo_description",
                table: "blogs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seo_keywords",
                table: "blogs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seo_page_title",
                table: "blogs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "blogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "thumbnail",
                table: "blogs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "view_count",
                table: "blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "home_flag",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "hot_flag",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "name",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "seo_alias",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "seo_description",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "seo_keywords",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "seo_page_title",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "status",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "thumbnail",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "view_count",
                table: "blogs");
        }
    }
}
