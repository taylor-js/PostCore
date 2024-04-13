using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_stat_statements", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "assetmgmt",
                columns: table => new
                {
                    uniqueassetid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    assetid = table.Column<long>(type: "bigint", nullable: false),
                    assettype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetmanufacturer = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetcategory = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetworkordernumber = table.Column<long>(type: "bigint", nullable: true),
                    assetpurchaseordernumber = table.Column<long>(type: "bigint", nullable: true),
                    assetdate = table.Column<DateOnly>(type: "date", nullable: true),
                    assetprojectmanager = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetequipmentamount = table.Column<decimal>(type: "money", nullable: true),
                    assetdescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assetmgmt_pkey", x => x.uniqueassetid);
                });

            migrationBuilder.CreateTable(
                name: "amcontent",
                columns: table => new
                {
                    uniquecontentid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    uniqueassetidcont = table.Column<Guid>(type: "uuid", nullable: true),
                    assetcontentnumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetcontentdescription = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    assetcontentversion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetcontentdateassigned = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amcontent_pkey", x => x.uniquecontentid);
                    table.ForeignKey(
                        name: "fk_amcontent_uniqueassetidcont",
                        column: x => x.uniqueassetidcont,
                        principalTable: "assetmgmt",
                        principalColumn: "uniqueassetid");
                });

            migrationBuilder.CreateTable(
                name: "amdistrib",
                columns: table => new
                {
                    uniquedistributionid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    uniqueassetiddistr = table.Column<Guid>(type: "uuid", nullable: true),
                    assetdistributionowner = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetdistributionlocation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    assetdistributionquantity = table.Column<int>(type: "integer", nullable: true),
                    assetdistributiondateassigned = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amdistrib_pkey", x => x.uniquedistributionid);
                    table.ForeignKey(
                        name: "fk_amdistrib_uniqueassetiddistr",
                        column: x => x.uniqueassetiddistr,
                        principalTable: "assetmgmt",
                        principalColumn: "uniqueassetid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_amcontent_uniqueassetidcont",
                table: "amcontent",
                column: "uniqueassetidcont");

            migrationBuilder.CreateIndex(
                name: "IX_amdistrib_uniqueassetiddistr",
                table: "amdistrib",
                column: "uniqueassetiddistr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amcontent");

            migrationBuilder.DropTable(
                name: "amdistrib");

            migrationBuilder.DropTable(
                name: "assetmgmt");
        }
    }
}
