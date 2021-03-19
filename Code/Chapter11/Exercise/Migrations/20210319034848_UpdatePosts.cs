using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise.Migrations
{
    public partial class UpdatePosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubBlogId",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubBlogId",
                table: "Posts",
                column: "SubBlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_SubBlogId",
                table: "Posts",
                column: "SubBlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_SubBlogId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubBlogId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubBlogId",
                table: "Posts");
        }
    }
}
