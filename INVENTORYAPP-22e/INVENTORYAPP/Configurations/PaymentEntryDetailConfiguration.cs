using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class PaymentEntryDetailConfiguration
    : IEntityTypeConfiguration<PaymentEntryDetail>
{
    public void Configure(
        EntityTypeBuilder<PaymentEntryDetail> builder)
    {
        builder.ToTable("cpydet", "tradinguser");

        // Primary Key
        builder.HasKey(x => new
        {
            x.DocNo,
            x.SlNo
        });

        // Id is database-generated and is not used by this application.
        builder.Ignore(x => x.Id);

        builder.Property(x => x.DocDate);

        builder.Property(x => x.STimestamp);

        // PartyId = AccountMaster.Id
        // C = Customer
        // S = Supplier
        builder.Property(x => x.PartyId);

        // AccountId = AccountMaster.Id
        // B = Bank/Cash
        builder.Property(x => x.AccountId);

        builder.HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.AcName)
            .HasMaxLength(100);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.PaymentEntry)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.DocNo)
            .OnDelete(DeleteBehavior.Cascade);
    }
}