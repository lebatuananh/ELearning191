using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class updateLearnerResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_responses_learner_id_lesson_id",
                table: "responses");

            migrationBuilder.CreateIndex(
                name: "ix_responses_learner_id",
                table: "responses",
                column: "learner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_responses_learner_id",
                table: "responses");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_responses_learner_id_lesson_id",
                table: "responses",
                columns: new[] { "learner_id", "lesson_id" });
        }
    }
}
