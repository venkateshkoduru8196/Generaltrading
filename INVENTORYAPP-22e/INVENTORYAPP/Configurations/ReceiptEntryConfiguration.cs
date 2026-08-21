using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class ReceiptEntryConfiguration
    : IEntityTypeConfiguration<ReceiptEntry>
{
    public void Configure(
        EntityTypeBuilder<ReceiptEntry> builder)
    {
        builder.ToTable("crc");

        // Primary Key
        builder.HasKey(x => x.DocNo);

        builder.Property(x => x.DocNo)
            .ValueGeneratedOnAdd();

        // Receipt
        builder.Property(x => x.DocDate);

        // AccountMaster.Id
        // C = Customer
        // S = Supplier
        builder.Property(x => x.PartyId);

        builder.Property(x => x.STimestamp);

        // No PartyMaster relationship.
        // PartyId refers to AccountMaster.Id.
    }
}