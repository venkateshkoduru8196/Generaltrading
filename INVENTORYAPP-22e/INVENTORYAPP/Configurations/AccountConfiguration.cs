using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("AccountMaster", "tradinguser");
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
        // Account Code
        //==========================================

        builder.Property(x => x.AccountCode)
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Account Name
        //==========================================

        builder.Property(x => x.AccountName)
            .HasMaxLength(150)
            .IsRequired();

        //==========================================
        // Account Type
        //
        // G = General
        // B = Bank/Cash
        // C = Customer
        // S = Supplier
        //==========================================

        builder.Property(x => x.Actype)
            .HasMaxLength(1)
            .IsRequired()
            .HasDefaultValue("G");

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
        //
        // Same account code can exist
        // in different companies.
        //
        // Only active/non-deleted records
        // participate in uniqueness.
        //==========================================

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.AccountCode
        })
        .IsUnique()
        .HasFilter("[IsActive] = 1 AND [IsDeleted] = 0");
    }
}