using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructorApp.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAssignments_Users_UserId",
                table: "ProjectAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectAssignments_UserId",
                table: "ProjectAssignments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Prize",
                table: "Projects",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ProjectAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignments_AppUserId",
                table: "ProjectAssignments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignments_AspNetUsers_AppUserId",
                table: "ProjectAssignments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAssignments_AspNetUsers_AppUserId",
                table: "ProjectAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectAssignments_AppUserId",
                table: "ProjectAssignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ProjectAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignments_UserId",
                table: "ProjectAssignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignments_Users_UserId",
                table: "ProjectAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
