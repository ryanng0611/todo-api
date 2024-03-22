using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastLoginDateTime",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "LastLoginDateTime");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
