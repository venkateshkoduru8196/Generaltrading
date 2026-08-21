using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class FixStockItemCodeUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster");

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster",
                columns: new[] { "CompanyId", "stkcode" },
                unique: true,
                filter: "[isactive] = 1 AND [isdeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster");

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster",
                columns: new[] { "CompanyId", "stkcode" },
                unique: true);
        }
    }
}
