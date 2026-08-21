using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    public partial class AddReceiptTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =========================================================
            // Receipt Header
            // tradinguser.crc
            // =========================================================

            migrationBuilder.CreateTable(
                name: "crc",
                schema: "tradinguser",
                columns: table => new
                {
                    DocNo = table.Column<long>(
                        type: "bigint",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    DocDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    PartyId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    STimestamp = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    IsActive = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: true),

                    IsDeleted = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: false),

                    CreatedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    CreatedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    ModifiedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    ModifiedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    DeletedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    DeletedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_crc",
                        x => x.DocNo);

                    table.ForeignKey(
                        name: "FK_crc_PartyMaster_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "tradinguser",
                        principalTable: "PartyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // =========================================================
            // Receipt Details
            // tradinguser.crcdet
            // =========================================================

            migrationBuilder.CreateTable(
                name: "crcdet",
                schema: "tradinguser",
                columns: table => new
                {
                    // This was missing from the previous table
                    Id = table.Column<long>(
                        type: "bigint",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    DocNo = table.Column<long>(
                        type: "bigint",
                        nullable: false),

                    SlNo = table.Column<int>(
                        type: "int",
                        nullable: false),

                    DocDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    PartyId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    STimestamp = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    AccountId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    AcName = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false),

                    Amount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        precision: 18,
                        scale: 2,
                        nullable: false),

                    IsActive = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: true),

                    IsDeleted = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: false),

                    CreatedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    CreatedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    ModifiedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    ModifiedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    DeletedOn = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    DeletedBy = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_crcdet",
                        x => new { x.DocNo, x.SlNo });

                    table.ForeignKey(
                        name: "FK_crcdet_crc_DocNo",
                        column: x => x.DocNo,
                        principalSchema: "tradinguser",
                        principalTable: "crc",
                        principalColumn: "DocNo",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_crcdet_PartyMaster_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "tradinguser",
                        principalTable: "PartyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_crcdet_AccountMaster_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "tradinguser",
                        principalTable: "AccountMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // =========================================================
            // Indexes
            // =========================================================

            migrationBuilder.CreateIndex(
                name: "IX_crc_PartyId",
                schema: "tradinguser",
                table: "crc",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_crcdet_PartyId",
                schema: "tradinguser",
                table: "crcdet",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_crcdet_AccountId",
                schema: "tradinguser",
                table: "crcdet",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crcdet",
                schema: "tradinguser");

            migrationBuilder.DropTable(
                name: "crc",
                schema: "tradinguser");
        }
    }
}