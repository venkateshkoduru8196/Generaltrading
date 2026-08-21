using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gsal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    docno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    docdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partycode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gsal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gsaldet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    docno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    docdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partycode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    slno = table.Column<int>(type: "int", nullable: false),
                    stkcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    stkname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxableamt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxrate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    taxamt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GSalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gsaldet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gsaldet_gsal_GSalId",
                        column: x => x.GSalId,
                        principalTable: "gsal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gsal_docno",
                table: "gsal",
                column: "docno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gsaldet_GSalId",
                table: "gsaldet",
                column: "GSalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gsaldet");

            migrationBuilder.DropTable(
                name: "gsal");
        }
    }
}
