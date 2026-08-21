using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddMetalMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "metalmaster",
                columns: table => new
                {
                    stkcode = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    stkname = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    mtltype = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    karat = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    purity = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    spurity = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    karatcat = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    cat = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ispcs = table.Column<short>(type: "smallint", nullable: true),
                    isweight = table.Column<short>(type: "smallint", nullable: true),
                    uom = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    lcccode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    mkglcccode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metalmaster", x => x.stkcode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_metalmaster_cat",
                table: "metalmaster",
                column: "cat");

            migrationBuilder.CreateIndex(
                name: "IX_metalmaster_karat",
                table: "metalmaster",
                column: "karat");

            migrationBuilder.CreateIndex(
                name: "IX_metalmaster_karatcat",
                table: "metalmaster",
                column: "karatcat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "metalmaster");
        }
    }
}
