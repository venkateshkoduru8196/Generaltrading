using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class ItemConfiguration
    : IEntityTypeConfiguration<Item>
{
    public void Configure(
        EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("MST_ITEM");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CgstPer)
            .HasPrecision(18, 2);

        builder.Property(x => x.SgstPer)
            .HasPrecision(18, 2);

        builder.Property(x => x.IgstPer)
            .HasPrecision(18, 2);

        builder.Property(x => x.PRate)
            .HasPrecision(18, 2);

        builder.Property(x => x.SRate)
            .HasPrecision(18, 2);

        builder.Property(x => x.Mrp)
            .HasPrecision(18, 2);
    }
}


