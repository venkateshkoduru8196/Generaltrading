using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class RoleMenuConfiguration
    : IEntityTypeConfiguration<RoleMenu>
{
    public void Configure(
        EntityTypeBuilder<RoleMenu> builder)
    {
        builder.HasKey(x => x.RoleMenuId);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Menu)
            .WithMany()
            .HasForeignKey(x => x.MenuId);
    }
}