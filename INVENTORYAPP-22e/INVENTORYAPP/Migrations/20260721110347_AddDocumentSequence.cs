using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentSequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinancialYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false),
                    Digits = table.Column<int>(type: "int", nullable: false, defaultValue: 6),
                    Separator = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, defaultValue: ""),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSequences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSequences_ModuleCode_FinancialYear_BranchCode",
                table: "DocumentSequences",
                columns: new[] { "ModuleCode", "FinancialYear", "BranchCode" },
                unique: true,
                filter: "[BranchCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentSequences");
        }
    }
}
