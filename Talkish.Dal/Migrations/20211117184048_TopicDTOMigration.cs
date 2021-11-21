using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class TopicDTOMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Topics_TopicId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_TopicId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_BlogId",
                table: "Topics",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Blogs_BlogId",
                table: "Topics",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Blogs_BlogId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_BlogId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Topics");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TopicId",
                table: "Blogs",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Topics_TopicId",
                table: "Blogs",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
