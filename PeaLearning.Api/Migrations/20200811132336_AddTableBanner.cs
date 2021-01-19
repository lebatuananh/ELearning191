using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class AddTableBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banners",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<Guid>(nullable: false),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: false),
                    thumbnail = table.Column<string>(maxLength: 256, nullable: true),
                    link = table.Column<string>(maxLength: 500, nullable: true),
                    banner_position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banners", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banners");
        }
    }
}
