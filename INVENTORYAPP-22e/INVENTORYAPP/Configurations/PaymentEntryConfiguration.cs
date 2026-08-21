using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class PaymentEntryConfiguration
    : IEntityTypeConfiguration<PaymentEntry>
{
    public void Configure(
        EntityTypeBuilder<PaymentEntry> builder)
    {
        builder.ToTable("cpy", "tradinguser");

        //==========================================
        // Primary Key
        //==========================================

        builder.HasKey(x => x.DocNo);

        builder.Property(x => x.DocNo)
            .ValueGeneratedOnAdd();

        //==========================================
        // Payment
        //==========================================

        builder.Property(x => x.DocDate);

        builder.Property(x => x.PartyId);

        builder.Property(x => x.STimestamp);

        //==========================================
        // Party
        //==========================================
        //
        // PartyId stores AccountMaster.Id
        //
        // C = Customer
        // S = Supplier
        //
        // No EF Party relationship.
        //==========================================

        // PartyId is only an AccountMaster.Id.
        // Validation is handled in Payment business logic.
    }
}