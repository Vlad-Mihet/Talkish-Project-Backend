using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class RemoveAuthUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUsers");

            migrationBuilder.AddColumn<int>(
                name: "BasicInfoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BasicInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BasicInfoId",
                table: "Users",
                column: "BasicInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BasicInfo_BasicInfoId",
                table: "Users",
                column: "BasicInfoId",
                principalTable: "BasicInfo",
                principalColumn: "BasicInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BasicInfo_BasicInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BasicInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BasicInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BasicInfo");

            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    IdentityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.IdentityId);
                });
        }
    }
}
