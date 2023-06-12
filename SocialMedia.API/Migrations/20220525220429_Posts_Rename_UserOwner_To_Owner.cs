using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Infrastructure.Migrations
{
    public partial class Posts_Rename_UserOwner_To_Owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_OwnerId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_OwnerId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
