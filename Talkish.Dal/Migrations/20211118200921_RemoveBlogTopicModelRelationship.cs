using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class RemoveBlogTopicModelRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BlogTopic",
                columns: table => new
                {
                    BlogsBlogId = table.Column<int>(type: "int", nullable: false),
                    TopicsTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTopic", x => new { x.BlogsBlogId, x.TopicsTopicId });
                    table.ForeignKey(
                        name: "FK_BlogTopic_Blogs_BlogsBlogId",
                        column: x => x.BlogsBlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTopic_Topics_TopicsTopicId",
                        column: x => x.TopicsTopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTopic_TopicsTopicId",
                table: "BlogTopic",
                column: "TopicsTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTopic");

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
    }
}
