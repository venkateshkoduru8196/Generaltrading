using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class MetalMasterConfiguration : IEntityTypeConfiguration<MetalMaster>
{
    public void Configure(EntityTypeBuilder<MetalMaster> builder)
    {
        // Table Name
        builder.ToTable("metalmaster");

        // Primary Key
        builder.HasKey(x => x.StkCode);

        // Properties
        builder.Property(x => x.StkCode)
               .HasColumnName("stkcode")
               .HasMaxLength(55)
               .IsRequired();

        builder.Property(x => x.StkName)
               .HasColumnName("stkname")
               .HasMaxLength(55);

        builder.Property(x => x.MtlType)
               .HasColumnName("mtltype")
               .HasMaxLength(1);

        builder.Property(x => x.Karat)
               .HasColumnName("karat")
               .HasMaxLength(2);

        builder.Property(x => x.Purity)
               .HasColumnName("purity")
               .HasPrecision(14, 2);

        builder.Property(x => x.SPurity)
               .HasColumnName("spurity")
               .HasPrecision(14, 2);

        builder.Property(x => x.KaratCat)
               .HasColumnName("karatcat")
               .HasMaxLength(55);

        builder.Property(x => x.Cat)
               .HasColumnName("cat")
               .HasMaxLength(8);

        builder.Property(x => x.IsPcs)
               .HasColumnName("ispcs");

        builder.Property(x => x.IsWeight)
               .HasColumnName("isweight");

        builder.Property(x => x.Uom)
               .HasColumnName("uom")
               .HasMaxLength(3);

        builder.Property(x => x.LccCode)
               .HasColumnName("lcccode")
               .HasMaxLength(4);

        builder.Property(x => x.MkgLccCode)
               .HasColumnName("mkglcccode")
               .HasMaxLength(4);

        // Indexes
        builder.HasIndex(x => x.Cat);

        builder.HasIndex(x => x.Karat);

        builder.HasIndex(x => x.KaratCat);
    }
}