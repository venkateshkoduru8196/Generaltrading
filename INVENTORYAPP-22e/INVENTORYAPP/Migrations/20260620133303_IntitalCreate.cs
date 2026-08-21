using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class IntitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MST_ITEM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatId = table.Column<long>(type: "bigint", nullable: true),
                    CgstPer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SgstPer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    STaxIncl = table.Column<bool>(type: "bit", nullable: true),
                    PTaxIncl = table.Column<bool>(type: "bit", nullable: true),
                    ManufId = table.Column<long>(type: "bigint", nullable: true),
                    MainUnit = table.Column<long>(type: "bigint", nullable: true),
                    Rack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRate = table.Column<decimal>(type: "decimal(18 2)", nullable: true),
                    SRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Mrp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    IgstPer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Rol = table.Column<double>(type: "float", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpiry = table.Column<bool>(type: "bit", nullable: true),
                    HsnCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefBarcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deactivate = table.Column<bool>(type: "bit", nullable: true),
                    CessPer = table.Column<double>(type: "float", nullable: true),
                    AddCess = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_ITEM", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MST_ITEM");
        }
    }
}
