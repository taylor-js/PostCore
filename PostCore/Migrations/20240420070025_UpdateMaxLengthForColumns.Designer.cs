﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PostCore.Models;

#nullable disable

namespace PostCore.Migrations
{
    [DbContext(typeof(D2glkvqrc1vuvsContext))]
    [Migration("20240420070025_UpdateMaxLengthForColumns")]
    partial class UpdateMaxLengthForColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_stat_statements");
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PostCore.Models.Amcontent", b =>
                {
                    b.Property<Guid>("Uniquecontentid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("uniquecontentid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateOnly>("Assetcontentdateassigned")
                        .HasColumnType("date")
                        .HasColumnName("assetcontentdateassigned");

                    b.Property<string>("Assetcontentdescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("assetcontentdescription");

                    b.Property<string>("Assetcontentnumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("assetcontentnumber");

                    b.Property<string>("Assetcontentversion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("assetcontentversion");

                    b.Property<Guid>("Uniqueassetidcont")
                        .HasColumnType("uuid")
                        .HasColumnName("uniqueassetidcont");

                    b.HasKey("Uniquecontentid")
                        .HasName("amcontent_pkey");

                    b.HasIndex("Uniqueassetidcont");

                    b.ToTable("amcontent", (string)null);
                });

            modelBuilder.Entity("PostCore.Models.Amdistrib", b =>
                {
                    b.Property<Guid>("Uniquedistributionid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("uniquedistributionid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateOnly>("Assetdistributiondateassigned")
                        .HasColumnType("date")
                        .HasColumnName("assetdistributiondateassigned");

                    b.Property<string>("Assetdistributionlocation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("assetdistributionlocation");

                    b.Property<string>("Assetdistributionowner")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("assetdistributionowner");

                    b.Property<int>("Assetdistributionquantity")
                        .HasPrecision(10)
                        .HasColumnType("integer")
                        .HasColumnName("assetdistributionquantity");

                    b.Property<Guid>("Uniqueassetiddistr")
                        .HasColumnType("uuid")
                        .HasColumnName("uniqueassetiddistr");

                    b.HasKey("Uniquedistributionid")
                        .HasName("amdistrib_pkey");

                    b.HasIndex("Uniqueassetiddistr");

                    b.ToTable("amdistrib", (string)null);
                });

            modelBuilder.Entity("PostCore.Models.AssetMgmt", b =>
                {
                    b.Property<Guid>("Uniqueassetid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("uniqueassetid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Assetcategory")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("assetcategory");

                    b.Property<DateOnly>("Assetdate")
                        .HasColumnType("date")
                        .HasColumnName("assetdate");

                    b.Property<string>("Assetdescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("assetdescription");

                    b.Property<decimal>("Assetequipmentamount")
                        .HasColumnType("money")
                        .HasColumnName("assetequipmentamount");

                    b.Property<long>("Assetid")
                        .HasPrecision(5)
                        .HasColumnType("bigint")
                        .HasColumnName("assetid");

                    b.Property<string>("Assetmanufacturer")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("assetmanufacturer");

                    b.Property<string>("Assetname")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("assetname");

                    b.Property<string>("Assetprojectmanager")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("assetprojectmanager");

                    b.Property<long>("Assetpurchaseordernumber")
                        .HasPrecision(10)
                        .HasColumnType("bigint")
                        .HasColumnName("assetpurchaseordernumber");

                    b.Property<string>("Assettype")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("assettype");

                    b.Property<long>("Assetworkordernumber")
                        .HasPrecision(10)
                        .HasColumnType("bigint")
                        .HasColumnName("assetworkordernumber");

                    b.HasKey("Uniqueassetid")
                        .HasName("assetmgmt_pkey");

                    b.ToTable("assetmgmt", (string)null);
                });

            modelBuilder.Entity("PostCore.Models.Amcontent", b =>
                {
                    b.HasOne("PostCore.Models.AssetMgmt", "UniqueassetidcontNavigation")
                        .WithMany("Amcontents")
                        .HasForeignKey("Uniqueassetidcont")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_amcontent_uniqueassetidcont");

                    b.Navigation("UniqueassetidcontNavigation");
                });

            modelBuilder.Entity("PostCore.Models.Amdistrib", b =>
                {
                    b.HasOne("PostCore.Models.AssetMgmt", "UniqueassetiddistrNavigation")
                        .WithMany("Amdistribs")
                        .HasForeignKey("Uniqueassetiddistr")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_amdistrib_uniqueassetiddistr");

                    b.Navigation("UniqueassetiddistrNavigation");
                });

            modelBuilder.Entity("PostCore.Models.AssetMgmt", b =>
                {
                    b.Navigation("Amcontents");

                    b.Navigation("Amdistribs");
                });
#pragma warning restore 612, 618
        }
    }
}
