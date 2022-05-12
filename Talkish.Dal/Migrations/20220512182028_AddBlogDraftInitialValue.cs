using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Talkish.Dal.Migrations
{
    public partial class AddBlogDraftInitialValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Publications",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDraft",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Blogs");
        }
    }
}
