using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class RemoveUserOwnBasicInfoRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicInfo_Users_UserId",
                table: "BasicInfo");

            migrationBuilder.DropIndex(
                name: "IX_BasicInfo_UserId",
                table: "BasicInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BasicInfo");

            migrationBuilder.AddColumn<int>(
                name: "BasicInfoId",
                table: "Users",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BasicInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasicInfo_UserId",
                table: "BasicInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasicInfo_Users_UserId",
                table: "BasicInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
