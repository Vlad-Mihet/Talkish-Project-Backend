using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class CreatedPublicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    PublicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.PublicationId);
                    table.ForeignKey(
                        name: "FK_Publications_Authors_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PublicationId",
                table: "Blogs",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PublicationId",
                table: "Authors",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_Name",
                table: "Publications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_OwnerId",
                table: "Publications",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Publications_PublicationId",
                table: "Authors",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Publications_PublicationId",
                table: "Blogs",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Publications_PublicationId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Publications_PublicationId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_PublicationId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Authors_PublicationId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Authors");
        }
    }
}
