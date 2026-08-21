using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class SalesAuditAndBranchUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMaster_stkcode",
                table: "StockMaster");

            migrationBuilder.DropIndex(
                name: "IX_gsal_docno",
                table: "gsal");

            migrationBuilder.EnsureSchema(
                name: "tradinguser");

            migrationBuilder.RenameTable(
                name: "accmst",
                newName: "accmst",
                newSchema: "tradinguser");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "StockMaster",
                newName: "modifiedon");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "StockMaster",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "StockMaster",
                newName: "createdon");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "gsaldet",
                newName: "modifiedon");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "gsaldet",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "gsaldet",
                newName: "createdon");

            migrationBuilder.RenameColumn(
                name: "unit",
                table: "gsaldet",
                newName: "unitcode");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "gsal",
                newName: "modifiedon");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "gsal",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "gsal",
                newName: "createdon");

            migrationBuilder.AlterColumn<string>(
                name: "stkname",
                table: "StockMaster",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "stkcode",
                table: "StockMaster",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "isactive",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "brncode",
                table: "StockMaster",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "StockMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deletedby",
                table: "StockMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedon",
                table: "StockMaster",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "modifiedby",
                table: "StockMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "taxrate",
                table: "StockMaster",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "brncode",
                table: "gsaldet",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "gsaldet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deletedby",
                table: "gsaldet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedon",
                table: "gsaldet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "gsaldet",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "gsaldet",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "modifiedby",
                table: "gsaldet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitname",
                table: "gsaldet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "brncode",
                table: "gsal",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "gsal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deletedby",
                table: "gsal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedon",
                table: "gsal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "gsal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "modifiedby",
                table: "gsal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcName",
                schema: "tradinguser",
                table: "accmst",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_brncode_stkcode",
                table: "StockMaster",
                columns: new[] { "brncode", "stkcode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gsal_brncode_docno",
                table: "gsal",
                columns: new[] { "brncode", "docno" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMaster_brncode_stkcode",
                table: "StockMaster");

            migrationBuilder.DropIndex(
                name: "IX_gsal_brncode_docno",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "deletedby",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "deletedon",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "modifiedby",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "taxrate",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "deletedby",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "deletedon",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "description",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "modifiedby",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "unitname",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "deletedby",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "deletedon",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "modifiedby",
                table: "gsal");

            migrationBuilder.RenameTable(
                name: "accmst",
                schema: "tradinguser",
                newName: "accmst");

            migrationBuilder.RenameColumn(
                name: "modifiedon",
                table: "StockMaster",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "StockMaster",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "StockMaster",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "modifiedon",
                table: "gsaldet",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "gsaldet",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "gsaldet",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "unitcode",
                table: "gsaldet",
                newName: "unit");

            migrationBuilder.RenameColumn(
                name: "modifiedon",
                table: "gsal",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "gsal",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "gsal",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<string>(
                name: "stkname",
                table: "StockMaster",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "stkcode",
                table: "StockMaster",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AcName",
                table: "accmst",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_stkcode",
                table: "StockMaster",
                column: "stkcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gsal_docno",
                table: "gsal",
                column: "docno",
                unique: true);
        }
    }
}
