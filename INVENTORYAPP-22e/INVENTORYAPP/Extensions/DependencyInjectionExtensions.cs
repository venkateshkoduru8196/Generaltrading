using INVENTORYAPP.Features.Authentication.Interfaces;
using INVENTORYAPP.Features.Authentication.Services;
using INVENTORYAPP.Features.Companies.Interfaces;
using INVENTORYAPP.Features.Companies.Repositories;
using INVENTORYAPP.Features.Companies.Services;
using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
using INVENTORYAPP.Features.Masters.Accounts.Repositories;
using INVENTORYAPP.Features.Masters.Accounts.Services;
using INVENTORYAPP.Features.Masters.Items.Interface;
using INVENTORYAPP.Features.Masters.Items.Repositories;
using INVENTORYAPP.Features.Masters.Items.Services;
using INVENTORYAPP.Features.Masters.Parties.Interfaces;
using INVENTORYAPP.Features.Masters.Parties.Repositories;
using INVENTORYAPP.Features.Masters.Parties.Services;
using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
using INVENTORYAPP.Features.Masters.StockItems.Repositories;
using INVENTORYAPP.Features.Masters.StockItems.Services;
using INVENTORYAPP.Features.Masters.Units.Interfaces;
using INVENTORYAPP.Features.Masters.Units.Repositories;
using INVENTORYAPP.Features.Masters.Units.Services;
using INVENTORYAPP.Features.Payments.Interface;
using INVENTORYAPP.Features.Payments.Repositories;
using INVENTORYAPP.Features.Payments.Services;
using INVENTORYAPP.Features.Receipts.Interface;
using INVENTORYAPP.Features.Receipts.Repositories;
using INVENTORYAPP.Features.Receipts.Services;
using INVENTORYAPP.Features.Reports.BusinessReport.Export.Excel;
using INVENTORYAPP.Features.Reports.BusinessReport.Export.Pdf;
using INVENTORYAPP.Features.Reports.BusinessReport.Export.Word;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Features.Reports.BusinessReport.Repositories;
using INVENTORYAPP.Features.Reports.BusinessReport.Services;
using INVENTORYAPP.Features.Sales.Export.Services;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Sales.Interfaces.Export;
using INVENTORYAPP.Features.Sales.Repositories;
using INVENTORYAPP.Features.Sales.Services;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Services;
using INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;
using INVENTORYAPP.Features.Shared.DocumentNumbers.Repositories;
using INVENTORYAPP.Features.Shared.DocumentNumbers.Services;
using INVENTORYAPP.Infrastructure.Jwt;
using INVENTORYAPP.Repositories;
using INVENTORYAPP.Repositories.Interfaces;
using INVENTORYAPP.Services;
using INVENTORYAPP.Services.Interfaces;



using INVENTORYAPP.Features.UserManagement.Interfaces;
using INVENTORYAPP.Features.UserManagement.Repositories;
using INVENTORYAPP.Features.UserManagement.Services;



//using INVENTORYAPP.Features.Payments.Interface;
//using INVENTORYAPP.Features.Payments.Repositories;
//using INVENTORYAPP.Features.Payments.Services;


//using INVENTORYAPP.Features.Receipts.Interface;
//using INVENTORYAPP.Features.Receipts.Repositories;
//using INVENTORYAPP.Features.Receipts.Services;





namespace INVENTORYAPP.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(
        this IServiceCollection services)
    {
        // Item
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();

        // Account
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountService, AccountService>();

        // Menu
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IMenuService, MenuService>();

        // Authentication
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtTokenGenerator>();

        // Reports
        services.AddScoped<IBusinessReportRepository, BusinessReportRepository>();
        services.AddScoped<IBusinessReportService, BusinessReportService>();

        services.AddScoped<IBusinessReportPdfService, BusinessReportPdfService>();
        services.AddScoped<IBusinessReportExcelService, BusinessReportExcelService>();
        services.AddScoped<IBusinessReportWordService, BusinessReportWordService>();


        //Stockitem

        services.AddScoped<IStockItemRepository, StockItemRepository>();

        services.AddScoped<IStockItemService, StockItemService>();



       //unit    
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IUnitService, UnitService>();

        //gsal

        services.AddScoped<IGSalRepository, GSalRepository>();
        services.AddScoped<IGSalService, GSalService>();

        //document number generation

        services.AddScoped<IDocumentSequenceRepository, DocumentSequenceRepository>();

        services.AddScoped<IDocumentNumberService, DocumentNumberService>();

        //loggeduser

        // Current User
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();




        //receipts

        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IReceiptService, ReceiptService>();
        ///payments
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        // Party
        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IPartyService, PartyService>();







        // ==========================================
        // Company
        // ==========================================

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyService, CompanyService>();

        //==========================================
        // Sales pdf download
        //==========================================

        services.AddScoped<IGSalPdfService, SalesInvoicePdfService>();

      //sales word download
        services.AddScoped<
    IGSalWordService,
    SalesInvoiceWordService>();

        //sales excel download

        services.AddScoped<
    IGSalExcelService,
    SalesInvoiceExcelService>();



    services.AddScoped<IUserRepository, UserRepository>();

    services.AddScoped<IUserService, UserService>();
















        return services;
    }
}