using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise.Migrations
{
    public partial class OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameMapId",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NameMapId",
                table: "Blogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NameMap",
                columns: table => new
                {
                    NameMapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameMap", x => x.NameMapId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_NameMapId",
                table: "Posts",
                column: "NameMapId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_NameMapId",
                table: "Blogs",
                column: "NameMapId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_NameMap_NameMapId",
                table: "Blogs",
                column: "NameMapId",
                principalTable: "NameMap",
                principalColumn: "NameMapId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_NameMap_NameMapId",
                table: "Posts",
                column: "NameMapId",
                principalTable: "NameMap",
                principalColumn: "NameMapId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_NameMap_NameMapId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_NameMap_NameMapId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "NameMap");

            migrationBuilder.DropIndex(
                name: "IX_Posts_NameMapId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_NameMapId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "NameMapId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "NameMapId",
                table: "Blogs");
        }
    }
}
