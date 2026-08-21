using INVENTORYAPP.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Data;

public class AppDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    string>
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<MenuMaster> MenuMasters { get; set; }

    public DbSet<RoleMenu> RoleMenus { get; set; }


    public DbSet<StockTransactionDetail> StockTransactionDetails { get; set; }


    public DbSet<MetalMaster> MetalMasters => Set<MetalMaster>();


    public DbSet<Account> Accounts { get; set; }

    public DbSet<StockItem> StockItems => Set<StockItem>();

    public DbSet<Unit> Units => Set<Unit>();


    public DbSet<GSal> GSales { get; set; }

    public DbSet<GSalDet> GSaleDetails { get; set; }

    public DbSet<DocumentSequence> DocumentSequences => Set<DocumentSequence>();




    // Receipt Module
    //public DbSet<ReceiptEntry> ReceiptEntries { get; set; }

    //public DbSet<ReceiptEntryDetail> ReceiptEntryDetails { get; set; }

    //public DbSet<PartyMaster> PartyMasters { get; set; }

    //public DbSet<AccountMaster> AccountMasters { get; set; }






    // Payment Module
    public DbSet<PaymentEntry> PaymentEntries { get; set; }

    public DbSet<PaymentEntryDetail> PaymentEntryDetails { get; set; }

    // Party Module
    public DbSet<Party> Parties { get; set; }


    // Receipt Module
    public DbSet<ReceiptEntry> ReceiptEntries { get; set; }

    public DbSet<ReceiptEntryDetail> ReceiptEntryDetails { get; set; }







    public DbSet<Company> Companies => Set<Company>();


    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Automatically load all IEntityTypeConfiguration classes
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);





    }
}





