using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class DocumentSequenceConfiguration
    : IEntityTypeConfiguration<DocumentSequence>
{
    public void Configure(
        EntityTypeBuilder<DocumentSequence> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("DocumentSequences");

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
        // Module Code
        //==========================================

        builder.Property(x => x.ModuleCode)
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Prefix
        //==========================================

        builder.Property(x => x.Prefix)
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Financial Year
        //==========================================

        builder.Property(x => x.FinancialYear)
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Current Number
        //==========================================

        builder.Property(x => x.CurrentNumber)
            .IsRequired();

        //==========================================
        // Digits
        //==========================================

        builder.Property(x => x.Digits)
            .HasDefaultValue(6)
            .IsRequired();

        //==========================================
        // Separator
        //==========================================

        builder.Property(x => x.Separator)
            .HasMaxLength(5)
            .HasDefaultValue(string.Empty);

        //==========================================
        // Active
        //==========================================

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        //==========================================
        // Created On
        //==========================================

        builder.Property(x => x.CreatedOn)
            .IsRequired();

        //==========================================
        // Modified On
        //==========================================

        builder.Property(x => x.ModifiedOn);

        //==========================================
        // UNIQUE
        // One sequence per Company
        // Module
        // Financial Year
        //==========================================

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.ModuleCode,
            x.FinancialYear
        })
        .IsUnique();
    }
}