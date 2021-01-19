using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class addQuestionContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "choice_content",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "correct_answer_id",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "question_type",
                table: "questions");

            migrationBuilder.AddColumn<string>(
                name: "question_content_raw",
                table: "questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "question_content_raw",
                table: "questions");

            migrationBuilder.AddColumn<string>(
                name: "choice_content",
                table: "questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "correct_answer_id",
                table: "questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "question_type",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
