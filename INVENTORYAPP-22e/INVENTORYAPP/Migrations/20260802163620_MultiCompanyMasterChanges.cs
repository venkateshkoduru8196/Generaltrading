using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class MultiCompanyMasterChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unit_BrnCode_code",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_StockMaster_brncode_stkcode",
                table: "StockMaster");

            migrationBuilder.DropIndex(
                name: "IX_gsal_brncode_docno",
                table: "gsal");

            migrationBuilder.DropIndex(
                name: "IX_DocumentSequences_ModuleCode_FinancialYear_BranchCode",
                table: "DocumentSequences");

            //migrationBuilder.DropIndex(
            //    name: "IX_AccountMaster_AccountCode",
            //    table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "BrnCode",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "brncode",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "DocumentSequences");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Unit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isactive",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "StockMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "companyid",
                table: "gsaldet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "companyid",
                table: "gsal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DocumentSequences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AccountMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccountMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccountMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AccountMaster",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccountMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AccountMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);



            //=====================================================
            // UPDATE EXISTING DATA
            //=====================================================

            migrationBuilder.Sql(
                "UPDATE Unit SET CompanyId = 1 WHERE CompanyId = 0");

            migrationBuilder.Sql(
                "UPDATE StockMaster SET CompanyId = 1 WHERE CompanyId = 0");

            migrationBuilder.Sql(
                "UPDATE AccountMaster SET CompanyId = 1 WHERE CompanyId = 0");

            migrationBuilder.Sql(
                "UPDATE DocumentSequences SET CompanyId = 1 WHERE CompanyId = 0");

            migrationBuilder.Sql(
                "UPDATE gsal SET companyid = 1 WHERE companyid = 0");

            migrationBuilder.Sql(
                "UPDATE gsaldet SET companyid = 1 WHERE companyid = 0");







            migrationBuilder.CreateIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit",
                columns: new[] { "CompanyId", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster",
                columns: new[] { "CompanyId", "stkcode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gsaldet_companyid",
                table: "gsaldet",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "IX_gsal_companyid_docno",
                table: "gsal",
                columns: new[] { "companyid", "docno" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSequences_CompanyId_ModuleCode_FinancialYear",
                table: "DocumentSequences",
                columns: new[] { "CompanyId", "ModuleCode", "FinancialYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster",
                columns: new[] { "CompanyId", "AccountCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMaster_Company_CompanyId",
                table: "AccountMaster",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSequences_Company_CompanyId",
                table: "DocumentSequences",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gsal_Company_companyid",
                table: "gsal",
                column: "companyid",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gsaldet_Company_companyid",
                table: "gsaldet",
                column: "companyid",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMaster_Company_CompanyId",
                table: "StockMaster",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Company_CompanyId",
                table: "Unit",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMaster_Company_CompanyId",
                table: "AccountMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSequences_Company_CompanyId",
                table: "DocumentSequences");

            migrationBuilder.DropForeignKey(
                name: "FK_gsal_Company_companyid",
                table: "gsal");

            migrationBuilder.DropForeignKey(
                name: "FK_gsaldet_Company_companyid",
                table: "gsaldet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMaster_Company_CompanyId",
                table: "StockMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Company_CompanyId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_CompanyId_code",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_StockMaster_CompanyId_stkcode",
                table: "StockMaster");

            migrationBuilder.DropIndex(
                name: "IX_gsaldet_companyid",
                table: "gsaldet");

            migrationBuilder.DropIndex(
                name: "IX_gsal_companyid_docno",
                table: "gsal");

            migrationBuilder.DropIndex(
                name: "IX_DocumentSequences_CompanyId_ModuleCode_FinancialYear",
                table: "DocumentSequences");

            migrationBuilder.DropIndex(
                name: "IX_AccountMaster_CompanyId_AccountCode",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "StockMaster");

            migrationBuilder.DropColumn(
                name: "companyid",
                table: "gsaldet");

            migrationBuilder.DropColumn(
                name: "companyid",
                table: "gsal");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DocumentSequences");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccountMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AccountMaster");

            migrationBuilder.AddColumn<string>(
                name: "BrnCode",
                table: "Unit",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "StockMaster",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

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
                name: "brncode",
                table: "gsaldet",
                type: "nvarchar(20)",
                maxLength: 20,
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
                name: "BranchCode",
                table: "DocumentSequences",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BrnCode_code",
                table: "Unit",
                columns: new[] { "BrnCode", "code" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSequences_ModuleCode_FinancialYear_BranchCode",
                table: "DocumentSequences",
                columns: new[] { "ModuleCode", "FinancialYear", "BranchCode" },
                unique: true,
                filter: "[BranchCode] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AccountMaster_AccountCode",
            //    table: "AccountMaster",
            //    column: "AccountCode",
            //    unique: true);
        }
    }
}
