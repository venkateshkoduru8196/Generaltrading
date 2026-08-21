using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accmst",
                columns: table => new
                {
                    AccCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accmst", x => x.AccCode);
                });

            migrationBuilder.CreateTable(
                name: "crc",
                columns: table => new
                {
                    DocNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crc", x => x.DocNo);
                });

            migrationBuilder.CreateTable(
                name: "crcdet",
                columns: table => new
                {
                    DocNo = table.Column<long>(type: "bigint", nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyCode = table.Column<long>(type: "bigint", nullable: false),
                    AccCode = table.Column<long>(type: "bigint", nullable: false),
                    AcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crcdet", x => new { x.DocNo, x.SlNo });
                });

            migrationBuilder.CreateTable(
                name: "partymst",
                columns: table => new
                {
                    PartyCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partymst", x => x.PartyCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accmst");

            migrationBuilder.DropTable(
                name: "crc");

            migrationBuilder.DropTable(
                name: "crcdet");

            migrationBuilder.DropTable(
                name: "partymst");
        }
    }
}
