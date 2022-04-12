using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class RemoveUserAuthorRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Authors_AuthorId",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Authors_UserId",
                table: "Authors");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Authors_AuthorId",
                table: "Users",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Authors_AuthorId",
                table: "Users");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Authors_AuthorId",
                table: "Users",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
