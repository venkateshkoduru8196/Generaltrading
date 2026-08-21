using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace INVENTORYAPP.Configurations;

public class GSalDetConfiguration : IEntityTypeConfiguration<GSalDet>
{
    public void Configure(EntityTypeBuilder<GSalDet> builder)
    {
        //==========================================
        // Table
        //==========================================

        builder.ToTable("gsaldet");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.Id);

        //==========================================
        // Sales Header FK
        //==========================================

        builder.Property(x => x.GSalId)
            .IsRequired();

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
        // Document Number
        //==========================================

        builder.Property(x => x.docno)
            .HasColumnName("docno")
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Document Date
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
        // Serial Number
        //==========================================

        builder.Property(x => x.slno)
            .HasColumnName("slno")
            .IsRequired();

        //==========================================
        // Stock Code
        //==========================================

        builder.Property(x => x.stkcode)
            .HasColumnName("stkcode")
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Stock Name
        //==========================================

        builder.Property(x => x.stkname)
            .HasColumnName("stkname")
            .HasMaxLength(150)
            .IsRequired();

        //==========================================
        // Description
        //==========================================

        builder.Property(x => x.description)
            .HasColumnName("description")
            .HasMaxLength(250);

        //==========================================
        // Unit Code
        //==========================================

        builder.Property(x => x.unitcode)
            .HasColumnName("unitcode")
            .HasMaxLength(20)
            .IsRequired();

        //==========================================
        // Unit Name
        //==========================================

        builder.Property(x => x.unitname)
            .HasColumnName("unitname")
            .HasMaxLength(100)
            .IsRequired();

        //==========================================
        // Quantity
        //==========================================

        builder.Property(x => x.qty)
            .HasColumnName("qty")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.rate)
            .HasColumnName("rate")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.taxableamt)
            .HasColumnName("taxableamt")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.taxrate)
            .HasColumnName("taxrate")
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.taxamt)
            .HasColumnName("taxamt")
            .HasColumnType("decimal(18,2)");

        //==========================================
        // Audit
        //==========================================

        builder.Property(x => x.IsActive)
            .HasColumnName("isactive")
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isdeleted")
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedOn)
            .HasColumnName("createdon")
            .IsRequired();

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
    }
}


