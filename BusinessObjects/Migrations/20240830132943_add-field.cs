using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class addfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginAttempts",
                table: "Users",
                newName: "FailedLoginAttempts");

            migrationBuilder.AddColumn<bool>(
                name: "IsBan",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBan",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FailedLoginAttempts",
                table: "Users",
                newName: "LoginAttempts");
        }
    }
}
