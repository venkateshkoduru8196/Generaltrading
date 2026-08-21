using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("Unit");

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
        // Unit Code
        //==========================================

        builder.Property(x => x.code)
            .HasMaxLength(50)
            .IsRequired();

        //==========================================
        // Description
        //==========================================

        builder.Property(x => x.description)
            .HasMaxLength(200)
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
        // Company + Unit Code
        // Only Active / Non-Deleted records
        // participate in unique constraint
        //==========================================

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.code
        })
        .IsUnique()
        .HasFilter(
            "[IsActive] = 1 AND [IsDeleted] = 0"
        );
    






}
}