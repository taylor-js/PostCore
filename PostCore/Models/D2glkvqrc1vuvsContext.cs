using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostCore.Models;

public partial class D2glkvqrc1vuvsContext : DbContext
{
    public D2glkvqrc1vuvsContext()
    {
    }

    public D2glkvqrc1vuvsContext(DbContextOptions<D2glkvqrc1vuvsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amcontent> Amcontents { get; set; }

    public virtual DbSet<Amdistrib> Amdistribs { get; set; }

    public virtual DbSet<AssetMgmt> AssetMgmts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_stat_statements")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Amcontent>(entity =>
        {
            entity.HasKey(e => e.Uniquecontentid).HasName("amcontent_pkey");

            entity.ToTable("amcontent");

            entity.Property(e => e.Uniquecontentid)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("uniquecontentid");

            entity.Property(e => e.Assetcontentdateassigned)
                .HasColumnName("assetcontentdateassigned");

            entity.Property(e => e.Assetcontentdescription)
                .HasMaxLength(100)
                .HasColumnName("assetcontentdescription");

            entity.Property(e => e.Assetcontentnumber)
                .HasMaxLength(50)
                .HasColumnName("assetcontentnumber");

            entity.Property(e => e.Assetcontentversion)
                .HasMaxLength(50)
                .HasColumnName("assetcontentversion");

            entity.Property(e => e.Uniqueassetidcont)
                .HasColumnName("uniqueassetidcont");

            entity.HasOne(d => d.UniqueassetidcontNavigation)
                .WithMany(p => p.Amcontents)
                .HasForeignKey(d => d.Uniqueassetidcont)
                .HasConstraintName("fk_amcontent_uniqueassetidcont");
        });

        modelBuilder.Entity<Amdistrib>(entity =>
        {
            entity.HasKey(e => e.Uniquedistributionid).HasName("amdistrib_pkey");

            entity.ToTable("amdistrib");

            entity.Property(e => e.Uniquedistributionid)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("uniquedistributionid");

            entity.Property(e => e.Assetdistributiondateassigned)
                .HasColumnName("assetdistributiondateassigned");

            entity.Property(e => e.Assetdistributionlocation)
                .HasMaxLength(50)
                .HasColumnName("assetdistributionlocation");

            entity.Property(e => e.Assetdistributionowner)
                .HasMaxLength(50)
                .HasColumnName("assetdistributionowner");

            entity.Property(e => e.Assetdistributionquantity)
                .HasColumnName("assetdistributionquantity");

            entity.Property(e => e.Uniqueassetiddistr)
                .HasColumnName("uniqueassetiddistr");

            entity.HasOne(d => d.UniqueassetiddistrNavigation)
                .WithMany(p => p.Amdistribs)
                .HasForeignKey(d => d.Uniqueassetiddistr)
                .HasConstraintName("fk_amdistrib_uniqueassetiddistr");
        });

        modelBuilder.Entity<AssetMgmt>(entity =>
        {
            entity.ToTable("assetmgmt");

            entity.Property(e => e.Uniqueassetid)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("uniqueassetid");

            entity.HasKey(e => e.Uniqueassetid)
                .HasName("assetmgmt_pkey");

            entity.Property(e => e.Assetid)
                .HasPrecision(5) //Precision should be 5 digits
                .HasColumnName("assetid");

            entity.Property(e => e.Assettype)
                .HasMaxLength(50)
                .HasColumnName("assettype");

            entity.Property(e => e.Assetname)
                .HasMaxLength(50)
                .HasColumnName("assetname");

            entity.Property(e => e.Assetmanufacturer)
                .HasMaxLength(50)
                .HasColumnName("assetmanufacturer");

            entity.Property(e => e.Assetcategory)
                .HasMaxLength(50)
                .HasColumnName("assetcategory");

            entity.Property(e => e.Assetworkordernumber)
                .HasPrecision(10) //Precision should be 10 digits
                .HasColumnName("assetworkordernumber");

            entity.Property(e => e.Assetpurchaseordernumber)
                .HasPrecision(10) //Precision should be 10 digits
                .HasColumnName("assetpurchaseordernumber");

            entity.Property(e => e.Assetdate)
                .HasColumnName("assetdate");

            entity.Property(e => e.Assetprojectmanager)
                .HasMaxLength(50)
                .HasColumnName("assetprojectmanager");

            entity.Property(e => e.Assetequipmentamount)
                .HasColumnType("money")
                .HasColumnName("assetequipmentamount");

            entity.Property(e => e.Assetdescription)
                .HasMaxLength(500)
                .HasColumnName("assetdescription");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
