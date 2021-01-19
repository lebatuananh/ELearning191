using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    course_id = table.Column<Guid>(nullable: false),
                    file_name = table.Column<string>(nullable: true),
                    file_size = table.Column<long>(nullable: false),
                    file_url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_attachments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<string>(nullable: true),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<string>(nullable: true),
                    friendly_uri = table.Column<string>(nullable: true),
                    code = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    thumbnail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.id);
                    table.UniqueConstraint("AK_courses_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    avatar = table.Column<string>(nullable: true),
                    gender = table.Column<bool>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    course_id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    lesson_type = table.Column<int>(nullable: false),
                    duration = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lessons", x => x.id);
                    table.ForeignKey(
                        name: "fk_lessons_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<string>(nullable: true),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<string>(nullable: true),
                    course_id = table.Column<Guid>(nullable: false),
                    learner_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_registrations", x => x.id);
                    table.UniqueConstraint("AK_course_registrations_learner_id_course_id", x => new { x.learner_id, x.course_id });
                    table.ForeignKey(
                        name: "fk_course_registrations_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_registrations_users_learner_id",
                        column: x => x.learner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    login_provider = table.Column<string>(nullable: true),
                    provider_key = table.Column<string>(nullable: true),
                    provider_display_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    login_provider = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    parent_id = table.Column<Guid>(nullable: false),
                    order_number = table.Column<int>(nullable: false),
                    example = table.Column<string>(nullable: true),
                    picture_url = table.Column<string>(nullable: true),
                    audio_url = table.Column<string>(nullable: true),
                    lesson_id = table.Column<Guid>(nullable: false),
                    question_type = table.Column<int>(nullable: false),
                    choice_content = table.Column<string>(nullable: true),
                    correct_answer_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_registrations_course_id",
                table: "course_registrations",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_course_id",
                table: "lessons",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_questions_lesson_id",
                table: "questions",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_name",
                unique: true,
                filter: "[normalized_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true,
                filter: "[normalized_user_name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_attachments");

            migrationBuilder.DropTable(
                name: "course_registrations");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "lessons");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
