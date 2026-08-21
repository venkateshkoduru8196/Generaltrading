using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class MenuMasterConfiguration
    : IEntityTypeConfiguration<MenuMaster>
{
    public void Configure(
        EntityTypeBuilder<MenuMaster> builder)
    {
        builder.HasKey(x => x.MenuId);

        builder.Property(x => x.MenuName)
            .HasMaxLength(100)
            .IsRequired();
    }
}