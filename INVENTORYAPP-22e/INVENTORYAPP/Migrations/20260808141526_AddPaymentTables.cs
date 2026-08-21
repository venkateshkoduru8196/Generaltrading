using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cpy",
                schema: "tradinguser",
                columns: table => new
                {
                    DocNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    STimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpy", x => x.DocNo);
                    table.ForeignKey(
                        name: "FK_cpy_PartyMaster_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "tradinguser",
                        principalTable: "PartyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cpydet",
                schema: "tradinguser",
                columns: table => new
                {
                    DocNo = table.Column<long>(type: "bigint", nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    STimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpydet", x => new { x.DocNo, x.SlNo });
                    table.ForeignKey(
                        name: "FK_cpydet_AccountMaster_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "tradinguser",
                        principalTable: "AccountMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cpydet_PartyMaster_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "tradinguser",
                        principalTable: "PartyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cpydet_cpy_DocNo",
                        column: x => x.DocNo,
                        principalSchema: "tradinguser",
                        principalTable: "cpy",
                        principalColumn: "DocNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cpy_PartyId",
                schema: "tradinguser",
                table: "cpy",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_cpydet_AccountId",
                schema: "tradinguser",
                table: "cpydet",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_cpydet_PartyId",
                schema: "tradinguser",
                table: "cpydet",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cpydet",
                schema: "tradinguser");

            migrationBuilder.DropTable(
                name: "cpy",
                schema: "tradinguser");
        }
    }
}
