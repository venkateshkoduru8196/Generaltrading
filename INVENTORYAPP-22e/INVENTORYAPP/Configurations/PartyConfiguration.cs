using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class PartyConfiguration : IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("PartyMaster", "tradinguser");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.Id);

        //==========================================
        // Company
        //==========================================

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        //==========================================
        // Party Code
        //==========================================

        builder.Property(x => x.PartyCode)
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Party Name
        //==========================================

        builder.Property(x => x.PartyName)
            .HasMaxLength(150)
            .IsRequired();

        //==========================================
        // Status
        //==========================================

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        //==========================================
        // Audit
        //==========================================

        builder.Property(x => x.CreatedOn)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(100);

        //==========================================
        // Unique
        // Same party code can exist
        // in different companies
        //==========================================

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.PartyCode
        }).IsUnique();
    }
}