using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddFilteredUniqueIndexToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit",
                columns: new[] { "CompanyId", "code" },
                unique: true,
                filter: "[IsActive] = 1 AND [IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit",
                columns: new[] { "CompanyId", "code" },
                unique: true);
        }
    }
}
