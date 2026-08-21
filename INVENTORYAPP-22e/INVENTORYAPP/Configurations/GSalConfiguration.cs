using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class GSalConfiguration : IEntityTypeConfiguration<GSal>
{
    public void Configure(EntityTypeBuilder<GSal> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("gsal");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.Id);

        //==========================================
        // Company
        //==========================================

        builder.Property(x => x.CompanyId)
            .HasColumnName("companyid")
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        //==========================================
        // Invoice Number
        //==========================================

        builder.Property(x => x.docno)
            .HasColumnName("docno")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.docno
        })
        .IsUnique();

        //==========================================
        // Invoice Date
        //==========================================

        builder.Property(x => x.docdate)
            .HasColumnName("docdate")
            .IsRequired();

        //==========================================
        // Timestamp
        //==========================================

        builder.Property(x => x.stimestamp)
            .HasColumnName("stimestamp")
            .IsRequired();

        //==========================================
        // Party Code
        //==========================================

        builder.Property(x => x.partycode)
            .HasColumnName("partycode")
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Is Active
        //==========================================

        builder.Property(x => x.IsActive)
            .HasColumnName("isactive")
            .HasDefaultValue(true);

        //==========================================
        // Is Deleted
        //==========================================

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isdeleted")
            .HasDefaultValue(false);

        //==========================================
        // Created On
        //==========================================

        builder.Property(x => x.CreatedOn)
            .HasColumnName("createdon")
            .IsRequired();

        //==========================================
        // Created By
        //==========================================

        builder.Property(x => x.CreatedBy)
            .HasColumnName("createdby")
            .HasMaxLength(100);

        //==========================================
        // Modified On
        //==========================================

        builder.Property(x => x.ModifiedOn)
            .HasColumnName("modifiedon");

        //==========================================
        // Modified By
        //==========================================

        builder.Property(x => x.ModifiedBy)
            .HasColumnName("modifiedby")
            .HasMaxLength(100);

        //==========================================
        // Deleted On
        //==========================================

        builder.Property(x => x.DeletedOn)
            .HasColumnName("deletedon");

        //==========================================
        // Deleted By
        //==========================================

        builder.Property(x => x.DeletedBy)
            .HasColumnName("deletedby")
            .HasMaxLength(100);

        //==========================================
        // Header -> Details
        //==========================================

        builder.HasMany(x => x.Details)
            .WithOne(x => x.GSal)
            .HasForeignKey(x => x.GSalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}