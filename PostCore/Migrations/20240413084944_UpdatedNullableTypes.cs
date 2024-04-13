using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNullableTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amcontent_uniqueassetidcont",
                table: "amcontent");

            migrationBuilder.DropForeignKey(
                name: "fk_amdistrib_uniqueassetiddistr",
                table: "amdistrib");

            migrationBuilder.AlterColumn<long>(
                name: "assetworkordernumber",
                table: "assetmgmt",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "assetpurchaseordernumber",
                table: "assetmgmt",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "assetequipmentamount",
                table: "assetmgmt",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetdate",
                table: "assetmgmt",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "uniqueassetiddistr",
                table: "amdistrib",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "assetdistributionquantity",
                table: "amdistrib",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetdistributiondateassigned",
                table: "amdistrib",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "uniqueassetidcont",
                table: "amcontent",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetcontentdateassigned",
                table: "amcontent",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_amcontent_uniqueassetidcont",
                table: "amcontent",
                column: "uniqueassetidcont",
                principalTable: "assetmgmt",
                principalColumn: "uniqueassetid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amdistrib_uniqueassetiddistr",
                table: "amdistrib",
                column: "uniqueassetiddistr",
                principalTable: "assetmgmt",
                principalColumn: "uniqueassetid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amcontent_uniqueassetidcont",
                table: "amcontent");

            migrationBuilder.DropForeignKey(
                name: "fk_amdistrib_uniqueassetiddistr",
                table: "amdistrib");

            migrationBuilder.AlterColumn<long>(
                name: "assetworkordernumber",
                table: "assetmgmt",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "assetpurchaseordernumber",
                table: "assetmgmt",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "assetequipmentamount",
                table: "assetmgmt",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetdate",
                table: "assetmgmt",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "uniqueassetiddistr",
                table: "amdistrib",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "assetdistributionquantity",
                table: "amdistrib",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetdistributiondateassigned",
                table: "amdistrib",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "uniqueassetidcont",
                table: "amcontent",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "assetcontentdateassigned",
                table: "amcontent",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "fk_amcontent_uniqueassetidcont",
                table: "amcontent",
                column: "uniqueassetidcont",
                principalTable: "assetmgmt",
                principalColumn: "uniqueassetid");

            migrationBuilder.AddForeignKey(
                name: "fk_amdistrib_uniqueassetiddistr",
                table: "amdistrib",
                column: "uniqueassetiddistr",
                principalTable: "assetmgmt",
                principalColumn: "uniqueassetid");
        }
    }
}
