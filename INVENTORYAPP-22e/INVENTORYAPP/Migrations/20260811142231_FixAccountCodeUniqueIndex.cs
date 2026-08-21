using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class FixAccountCodeUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster",
                columns: new[] { "CompanyId", "AccountCode" },
                unique: true,
                filter: "[IsActive] = 1 AND [IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster",
                columns: new[] { "CompanyId", "AccountCode" },
                unique: true);
        }
    }
}
