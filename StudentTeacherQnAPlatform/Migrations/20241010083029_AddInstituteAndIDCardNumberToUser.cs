using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTeacherQnAPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddInstituteAndIDCardNumberToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDCardNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Institute",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDCardNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Institute",
                table: "Users");
        }
    }
}
