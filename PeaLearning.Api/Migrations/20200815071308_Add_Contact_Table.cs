using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class Add_Contact_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<Guid>(nullable: false),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");
        }
    }
}
