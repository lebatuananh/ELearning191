using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class addLearnerResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "learner_id",
                table: "responses",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_responses_learner_id_lesson_id",
                table: "responses",
                columns: new[] { "learner_id", "lesson_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_responses_users_user_id",
                table: "responses",
                column: "learner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_responses_users_user_id",
                table: "responses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_responses_learner_id_lesson_id",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "learner_id",
                table: "responses");
        }
    }
}
