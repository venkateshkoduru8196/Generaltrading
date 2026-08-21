using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVENTORYAPP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnitModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_unitmst",
                table: "unitmst");

            migrationBuilder.DropIndex(
                name: "IX_unitmst_code",
                table: "unitmst");

            migrationBuilder.RenameTable(
                name: "unitmst",
                newName: "Unit");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Unit",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Unit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "BrnCode",
                table: "Unit",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Unit",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Unit",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Unit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Unit",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BrnCode_code",
                table: "Unit",
                columns: new[] { "BrnCode", "code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_BrnCode_code",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "BrnCode",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "unitmst");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "unitmst",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "unitmst",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_unitmst",
                table: "unitmst",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_unitmst_code",
                table: "unitmst",
                column: "code",
                unique: true);
        }
    }
}
