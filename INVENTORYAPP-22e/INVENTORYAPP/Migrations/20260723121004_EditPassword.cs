using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class EditPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditPasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditPasswordHash",
                table: "AspNetUsers");
        }
    }
}
