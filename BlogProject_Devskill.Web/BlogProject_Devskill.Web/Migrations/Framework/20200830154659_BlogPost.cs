using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject_Devskill.Web.Migrations.Framework
{
    public partial class BlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorImageUrl",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseAdminInfo",
                table: "BlogPosts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorImageUrl",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UseAdminInfo",
                table: "BlogPosts");
        }
    }
}
