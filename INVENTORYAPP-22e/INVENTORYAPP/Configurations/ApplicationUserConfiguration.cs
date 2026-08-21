using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class ApplicationUserConfiguration
    : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //=========================================
        // Full Name
        //=========================================

        builder.Property(x => x.FullName)
            .HasMaxLength(150)
            .IsRequired();

        //=========================================
        // Company
        //=========================================

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        //=========================================
        // Audit Fields
        //=========================================

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}