using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReceiptAccountRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //==========================================
            // Remove old PartyMaster relationships
            //==========================================

            migrationBuilder.DropForeignKey(
                name: "FK_crc_PartyMaster_PartyId",
                table: "crc");

            migrationBuilder.DropForeignKey(
                name: "FK_crcdet_PartyMaster_PartyId",
                schema: "tradinguser",
                table: "crcdet");

            //==========================================
            // Remove old AccountMaster table
            // accmst is no longer used
            //==========================================

            migrationBuilder.DropTable(
                name: "accmst",
                schema: "tradinguser");

            //==========================================
            // Remove old PartyMaster table
            // partymst is no longer used
            //==========================================

            migrationBuilder.DropTable(
                name: "partymst");

            //==========================================
            // Remove old PartyMaster indexes
            //==========================================

            migrationBuilder.DropIndex(
                name: "IX_crcdet_PartyId",
                schema: "tradinguser",
                table: "crcdet");

            migrationBuilder.DropIndex(
                name: "IX_crc_PartyId",
                table: "crc");

            //==========================================
            // IMPORTANT
            //
            // Do NOT rename AccountMaster.
            // tradinguser.AccountMaster already exists
            // and must remain unchanged.
            //
            // PartyId remains an existing integer column.
            // AccountId remains an existing integer column.
            //==========================================
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //==========================================
            // Re-create old accmst table
            //==========================================

            migrationBuilder.CreateTable(
                name: "accmst",
                schema: "tradinguser",
                columns: table => new
                {
                    AccCode = table.Column<long>(
                        type: "bigint",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    AcName = table.Column<string>(
                        type: "nvarchar(200)",
                        maxLength: 200,
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_accmst",
                        x => x.AccCode);
                });

            //==========================================
            // Re-create old partymst table
            //==========================================

            migrationBuilder.CreateTable(
                name: "partymst",
                columns: table => new
                {
                    PartyCode = table.Column<long>(
                        type: "bigint",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    PartyName = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_partymst",
                        x => x.PartyCode);
                });

            //==========================================
            // Re-create indexes
            //==========================================

            migrationBuilder.CreateIndex(
                name: "IX_crcdet_PartyId",
                schema: "tradinguser",
                table: "crcdet",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_crc_PartyId",
                table: "crc",
                column: "PartyId");

            //==========================================
            // Restore old PartyMaster relationships
            //==========================================

            migrationBuilder.AddForeignKey(
                name: "FK_crc_PartyMaster_PartyId",
                table: "crc",
                column: "PartyId",
                principalSchema: "tradinguser",
                principalTable: "PartyMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_crcdet_PartyMaster_PartyId",
                schema: "tradinguser",
                table: "crcdet",
                column: "PartyId",
                principalSchema: "tradinguser",
                principalTable: "PartyMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}