﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrecisionForDescr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "assetdescription",
                table: "assetmgmt",
                type: "character varying(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "assetdescription",
                table: "assetmgmt",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(600)",
                oldMaxLength: 600);
        }
    }
}
