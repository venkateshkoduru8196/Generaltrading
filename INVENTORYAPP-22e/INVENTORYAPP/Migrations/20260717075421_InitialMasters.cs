using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class InitialMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stkcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    stkname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMaster", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMaster_AccountCode",
                table: "AccountMaster",
                column: "AccountCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_stkcode",
                table: "StockMaster",
                column: "stkcode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMaster");

            migrationBuilder.DropTable(
                name: "StockMaster");
        }
    }
}
