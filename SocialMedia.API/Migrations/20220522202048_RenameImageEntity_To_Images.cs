using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Infrastructure.Migrations
{
    public partial class RenameImageEntity_To_Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageEntity_Posts_PostId",
                table: "ImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageEntity",
                table: "ImageEntity");

            migrationBuilder.RenameTable(
                name: "ImageEntity",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_ImageEntity_PostId",
                table: "Images",
                newName: "IX_Images_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Images_PostId",
                table: "ImageEntity",
                newName: "IX_ImageEntity_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageEntity",
                table: "ImageEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageEntity_Posts_PostId",
                table: "ImageEntity",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
