using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class rmRequireField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "lessons");

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "questions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "questions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "lessons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
