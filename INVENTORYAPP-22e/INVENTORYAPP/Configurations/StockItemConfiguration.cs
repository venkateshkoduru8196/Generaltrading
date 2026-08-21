using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class StockItemConfiguration
    : IEntityTypeConfiguration<StockItem>
{
    public void Configure(
        EntityTypeBuilder<StockItem> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("StockMaster");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

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
        // Stock Code
        //==========================================

        builder.Property(x => x.StockCode)
            .HasColumnName("stkcode")
            .HasMaxLength(50)
            .IsRequired();

        //==========================================
        // Stock Name
        //==========================================

        builder.Property(x => x.StockName)
            .HasColumnName("stkname")
            .HasMaxLength(200)
            .IsRequired();

        //==========================================
        // Tax Rate
        //==========================================

        builder.Property(x => x.TaxRate)
            .HasColumnName("taxrate")
            .HasPrecision(18, 2);

        //==========================================
        // Status
        //==========================================

        builder.Property(x => x.IsActive)
            .HasColumnName("isactive")
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isdeleted")
            .HasDefaultValue(false);

        //==========================================
        // Audit
        //==========================================

        builder.Property(x => x.CreatedOn)
            .HasColumnName("createdon");

        builder.Property(x => x.CreatedBy)
            .HasColumnName("createdby")
            .HasMaxLength(100);

        builder.Property(x => x.ModifiedOn)
            .HasColumnName("modifiedon");

        builder.Property(x => x.ModifiedBy)
            .HasColumnName("modifiedby")
            .HasMaxLength(100);

        builder.Property(x => x.DeletedOn)
            .HasColumnName("deletedon");

        builder.Property(x => x.DeletedBy)
            .HasColumnName("deletedby")
            .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.StockCode
        })
 .IsUnique()
 .HasFilter("[isactive] = 1 AND [isdeleted] = 0");
    }
}