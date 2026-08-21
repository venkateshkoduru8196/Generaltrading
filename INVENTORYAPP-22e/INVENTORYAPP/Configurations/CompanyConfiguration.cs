using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class CompanyConfiguration
    : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("Company");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.CompanyId);

        builder.Property(x => x.CompanyId)
            .ValueGeneratedOnAdd();

        //==========================================
        // Company Code
        //==========================================

        builder.Property(x => x.CompanyCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.CompanyCode)
            .IsUnique();

        //==========================================
        // Company Name
        //==========================================

        builder.Property(x => x.CompanyName)
            .HasMaxLength(200)
            .IsRequired();

        //==========================================
        // Owner Name
        //==========================================

        builder.Property(x => x.OwnerName)
            .HasMaxLength(150);

        //==========================================
        // GSTIN
        //==========================================

        builder.Property(x => x.GSTIN)
            .HasMaxLength(15);

        builder.HasIndex(x => x.GSTIN)
            .IsUnique();

        //==========================================
        // Phone
        //==========================================

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        //==========================================
        // Email
        //==========================================

        builder.Property(x => x.Email)
            .HasMaxLength(150);

        //==========================================
        // Address
        //==========================================

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        //==========================================
        // Active
        //==========================================

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        //==========================================
        // Created On
        //==========================================

        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        //==========================================
        // User Relationship
        //==========================================

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}