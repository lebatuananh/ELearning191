using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class addQuestionResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "responses",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<Guid>(nullable: false),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<Guid>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    submitted_date = table.Column<DateTimeOffset>(nullable: true),
                    lesson_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_responses", x => x.id);
                    table.ForeignKey(
                        name: "fk_responses_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_responses_lesson_id",
                table: "responses",
                column: "lesson_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "responses");
        }
    }
}
