using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeaLearning.Api.Migrations
{
    public partial class Create_Blog_BlogTag_Tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_tag",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "tags");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "tags",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "tags",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "tags",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tags",
                table: "tags",
                column: "id");

            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_date = table.Column<DateTimeOffset>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    created_by_id = table.Column<Guid>(nullable: false),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_by_id = table.Column<Guid>(nullable: false),
                    block_contents = table.Column<string>(nullable: true),
                    tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_tags",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    blog_id = table.Column<Guid>(nullable: false),
                    tag_id = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blog_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_blog_tags_blogs_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_blog_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_blog_tags_blog_id",
                table: "blog_tags",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "ix_blog_tags_tag_id",
                table: "blog_tags",
                column: "tag_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_tags");

            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tags",
                table: "tags");

            migrationBuilder.RenameTable(
                name: "tags",
                newName: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag",
                table: "Tags",
                column: "id");
        }
    }
}
