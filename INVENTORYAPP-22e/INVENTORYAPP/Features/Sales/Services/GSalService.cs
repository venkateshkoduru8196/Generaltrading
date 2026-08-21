using INVENTORYAPP.Features.Companies.Interfaces;
using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
using INVENTORYAPP.Features.Masters.Units.Interfaces;
using INVENTORYAPP.Features.Sales.DTOs;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;
using INVENTORYAPP.Models;
using INVENTORYAPP.Features.Sales.Export.DTOs;

namespace INVENTORYAPP.Features.Sales.Services;

public class GSalService : IGSalService
{
    private readonly IGSalRepository _repository;
    private readonly IStockItemRepository _stockRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IDocumentNumberService _documentNumberService;
    private readonly ICurrentUserService _currentUser;
    private readonly ICompanyRepository _companyRepository;

    public GSalService(
        IGSalRepository repository,
        IStockItemRepository stockRepository,
        IUnitRepository unitRepository,
        IAccountRepository accountRepository,
        IDocumentNumberService documentNumberService,
        ICurrentUserService currentUser,
        ICompanyRepository companyRepository)
    {
        _repository = repository;
        _stockRepository = stockRepository;
        _unitRepository = unitRepository;
        _accountRepository = accountRepository;
        _documentNumberService = documentNumberService;
        _currentUser = currentUser;
        _companyRepository = companyRepository;
    }

    //=====================================================
    // CREATE SALES INVOICE
    //=====================================================

    public async Task<GSalResponseDto> CreateAsync(
        GSalCreateRequestDto request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        int companyId = _currentUser.CompanyId.Value;

        if (string.IsNullOrWhiteSpace(request.PartyCode))
            throw new Exception("Party Code is required.");

        if (request.Details == null || !request.Details.Any())
            throw new Exception("Invoice must contain at least one item.");

        //==========================================
        // Validate Party
        //==========================================

        //var account =
        //    await _accountRepository.GetByCodeAsync(
        //        companyId,
        //        request.PartyCode);

        //if (account == null)
        //    throw new Exception(
        //        $"Invalid Party Code : {request.PartyCode}");


        var account =
    await _accountRepository.GetByCodeAsync(
        companyId,
        request.PartyCode);

        if (account == null)
            throw new Exception(
                $"Invalid Party Code : {request.PartyCode}");

        if (account.Actype is not ("C" or "S"))
            throw new Exception(
                $"Account '{request.PartyCode}' is not a valid Sales Party. " +
                "Only Customer (C) or Supplier (S) accounts are allowed.");






        //==========================================
        // Create Header
        //==========================================

        var sale = new GSal
        {
            CompanyId = companyId,

            docno = await _documentNumberService.GenerateAsync(
                companyId,
                "SAL"),

            docdate = request.DocDate,

            stimestamp = DateTime.Now,

            partycode = request.PartyCode,

            IsActive = true,

            IsDeleted = false,

            CreatedOn = DateTime.UtcNow,

            CreatedBy = _currentUser.UserName
        };

        foreach (var item in request.Details)
        {
            //==========================================
            // Validate Stock
            //==========================================

            var stock =
                await _stockRepository.GetByCodeAsync(
                    companyId,
                    item.StockCode);

            if (stock == null)
                throw new Exception(
                    $"Invalid Stock Code : {item.StockCode}");

            //==========================================
            // Validate Unit
            //==========================================

            var unit =
                await _unitRepository.GetByCodeAsync(
                    companyId,
                    item.UnitCode);

            if (unit == null)
                throw new Exception(
                    $"Invalid Unit Code : {item.UnitCode}");

            decimal amount = item.Qty * item.Rate;

            decimal taxableAmount = amount;

            decimal taxAmount =
                taxableAmount * item.TaxRate / 100;

            var detail = new GSalDet
            {
                CompanyId = companyId,

                docno = sale.docno,

                docdate = sale.docdate,

                stimestamp = sale.stimestamp,

                partycode = sale.partycode,

                slno = item.SlNo,

                stkcode = stock.StockCode,

                stkname = stock.StockName,

                description = item.Description,

                unitcode = unit.code,

                unitname = unit.description,

                qty = item.Qty,

                rate = item.Rate,

                amount = amount,

                taxableamt = taxableAmount,

                taxrate = item.TaxRate,

                taxamt = taxAmount,

                IsActive = true,

                IsDeleted = false,

                CreatedOn = DateTime.UtcNow,

                CreatedBy = _currentUser.UserName
            };

            sale.Details.Add(detail);
        }

        await _repository.AddAsync(sale);

        await _repository.SaveChangesAsync();

        return MapToResponseDto(sale);
    }


    //=====================================================
    // GET SALES BY ID
    //=====================================================

    public async Task<GSalResponseDto?> GetByIdAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        var sale = await _repository.GetByIdAsync(
            _currentUser.CompanyId.Value,
            id);

        if (sale == null)
            return null;

        return MapToResponseDto(sale);
    }

    //=====================================================
    // GET SALES BY DOCUMENT NUMBER
    //=====================================================

    public async Task<GSalResponseDto?> GetByDocNoAsync(
        string docNo)
    {
        if (string.IsNullOrWhiteSpace(docNo))
            throw new Exception("Document Number is required.");

        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        var sale = await _repository.GetByDocNoAsync(
            _currentUser.CompanyId.Value,
            docNo);

        if (sale == null)
            return null;

        return MapToResponseDto(sale);
    }

    //=====================================================
    // GET ALL SALES
    //=====================================================

    public async Task<List<GSalResponseDto>> GetAllAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        var sales = await _repository.GetAllAsync(
            _currentUser.CompanyId.Value);

        return sales
            .Select(MapToResponseDto)
            .ToList();
    }



    //=====================================================
    // GET INVOICE FOR EXPORT
    //=====================================================

    public async Task<SalesInvoiceExportDto?> GetInvoiceForExportAsync(
        int saleId)
    {
        //------------------------------------------
        // Company
        //------------------------------------------

        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        int companyId = _currentUser.CompanyId.Value;

        //------------------------------------------
        // Sale
        //------------------------------------------

        var sale = await _repository.GetByIdAsync(
            companyId,
            saleId);

        if (sale == null)
            return null;

        //------------------------------------------
        // Company
        //------------------------------------------

        var company =
            await _companyRepository.GetByIdAsync(companyId);

        if (company == null)
            throw new Exception("Company not found.");

        //------------------------------------------
        // Party
        //------------------------------------------

        var party =
            await _accountRepository.GetByCodeAsync(
                companyId,
                sale.partycode);

        //------------------------------------------
        // Build Export DTO
        //------------------------------------------

        var invoice = new SalesInvoiceExportDto
        {
            CompanyName = company.CompanyName,
            CompanyAddress = company.Address,
            CompanyPhone = company.PhoneNumber,
            CompanyEmail = company.Email,
            CompanyGSTIN = company.GSTIN,

            SaleId = sale.Id,
            InvoiceNo = sale.docno,
            InvoiceDate = sale.docdate,

            PartyCode = sale.partycode,
            PartyName = party?.AccountName ?? string.Empty,

            GeneratedBy = _currentUser.UserName,
            GeneratedOn = DateTime.Now
        };

        //------------------------------------------
        // Items
        //------------------------------------------

        invoice.Items = sale.Details
            .Where(x => x.IsActive && !x.IsDeleted)
            .OrderBy(x => x.slno)
            .Select(x => new SalesInvoiceItemExportDto
            {
                SlNo = x.slno,

                StockCode = x.stkcode,

                StockName = x.stkname,

                Description = x.description,

                Unit = x.unitname,

                Qty = x.qty,

                Rate = x.rate,

                Amount = x.amount,

                TaxableAmount = x.taxableamt,

                TaxRate = x.taxrate,

                TaxAmount = x.taxamt
            })
            .ToList();

        //------------------------------------------
        // Totals
        //------------------------------------------

        invoice.TotalQty =
            invoice.Items.Sum(x => x.Qty);

        invoice.TotalAmount =
            invoice.Items.Sum(x => x.Amount);

        invoice.TotalTax =
            invoice.Items.Sum(x => x.TaxAmount);

        invoice.GrandTotal =
            invoice.TotalAmount + invoice.TotalTax;

        //------------------------------------------
        // Return
        //------------------------------------------

        return invoice;
    }






    //=====================================================
    // UPDATE SALES INVOICE
    //=====================================================

    public async Task UpdateAsync(
        int id,
        GSalCreateRequestDto request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        int companyId = _currentUser.CompanyId.Value;

        var sale = await _repository.GetByIdAsync(
            companyId,
            id);

        if (sale == null)
            throw new Exception("Invoice not found.");

        if (string.IsNullOrWhiteSpace(request.PartyCode))
            throw new Exception("Party Code is required.");

        if (request.Details == null || !request.Details.Any())
            throw new Exception("Invoice must contain at least one item.");

        //==========================================
        // Validate Party
        //==========================================


        var account =
    await _accountRepository.GetByCodeAsync(
        companyId,
        request.PartyCode);

        if (account == null)
            throw new Exception(
                $"Invalid Party Code : {request.PartyCode}");

        if (account.Actype is not ("C" or "S"))
            throw new Exception(
                $"Account '{request.PartyCode}' is not a valid Sales Party. " +
                "Only Customer (C) or Supplier (S) accounts are allowed.");







        //var account =
        //    await _accountRepository.GetByCodeAsync(
        //        companyId,
        //        request.PartyCode);

        //if (account == null)
        //    throw new Exception(
        //        $"Invalid Party Code : {request.PartyCode}");

        //==========================================
        // UPDATE HEADER
        //==========================================

        sale.docdate = request.DocDate;

        sale.partycode = request.PartyCode;

        sale.ModifiedOn = DateTime.UtcNow;

        sale.ModifiedBy = _currentUser.UserName;

        //==========================================
        // SOFT DELETE OLD DETAILS
        //==========================================

        await _repository.SoftDeleteDetailsAsync(
            sale.Details.ToList(),
            _currentUser.UserName);

        await _repository.SaveChangesAsync();

        //==========================================
        // ADD NEW DETAILS
        //==========================================

        foreach (var item in request.Details)
        {
            var stock =
                await _stockRepository.GetByCodeAsync(
                    companyId,
                    item.StockCode);

            if (stock == null)
                throw new Exception(
                    $"Invalid Stock Code : {item.StockCode}");

            var unit =
                await _unitRepository.GetByCodeAsync(
                    companyId,
                    item.UnitCode);

            if (unit == null)
                throw new Exception(
                    $"Invalid Unit Code : {item.UnitCode}");

            decimal amount = item.Qty * item.Rate;

            decimal taxableAmount = amount;

            decimal taxAmount =
                taxableAmount * item.TaxRate / 100;

            var detail = new GSalDet
            {
                CompanyId = companyId,

                GSalId = sale.Id,

                docno = sale.docno,

                docdate = sale.docdate,

                stimestamp = DateTime.Now,

                partycode = sale.partycode,

                slno = item.SlNo,

                stkcode = stock.StockCode,

                stkname = stock.StockName,

                description = item.Description,

                unitcode = unit.code,

                unitname = unit.description,

                qty = item.Qty,

                rate = item.Rate,

                amount = amount,

                taxableamt = taxableAmount,

                taxrate = item.TaxRate,

                taxamt = taxAmount,

                IsActive = true,

                IsDeleted = false,

                CreatedOn = DateTime.UtcNow,

                CreatedBy = _currentUser.UserName
            };

            _repository.AddDetail(detail);
        }

        await _repository.UpdateAsync(sale);

        await _repository.SaveChangesAsync();
    }


    //=====================================================
    // DELETE SALES INVOICE
    //=====================================================

    public async Task DeleteAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found for logged in user.");

        int companyId = _currentUser.CompanyId.Value;

        var sale = await _repository.GetByIdAsync(
            companyId,
            id);

        if (sale == null)
            throw new Exception("Invoice not found.");

        //==========================================
        // Soft Delete Header
        //==========================================

        sale.IsActive = false;

        sale.IsDeleted = true;

        sale.ModifiedOn = DateTime.UtcNow;

        sale.ModifiedBy = _currentUser.UserName;

        sale.DeletedOn = DateTime.UtcNow;

        sale.DeletedBy = _currentUser.UserName;

        //==========================================
        // Soft Delete Details
        //==========================================

        foreach (var detail in sale.Details)
        {
            detail.IsActive = false;

            detail.IsDeleted = true;

            detail.ModifiedOn = DateTime.UtcNow;

            detail.ModifiedBy = _currentUser.UserName;

            detail.DeletedOn = DateTime.UtcNow;

            detail.DeletedBy = _currentUser.UserName;
        }

        await _repository.UpdateAsync(sale);

        await _repository.SaveChangesAsync();
    }

    //=====================================================
    // MAP ENTITY TO RESPONSE DTO
    //=====================================================

    private GSalResponseDto MapToResponseDto(
        GSal sale)
    {
        return new GSalResponseDto
        {
            Id = sale.Id,

            CompanyId = sale.CompanyId,

            DocNo = sale.docno,

            DocDate = sale.docdate,

            PartyCode = sale.partycode,

            IsActive = sale.IsActive,

            IsDeleted = sale.IsDeleted,

            CreatedOn = sale.CreatedOn,

            CreatedBy = sale.CreatedBy,

            ModifiedOn = sale.ModifiedOn,

            ModifiedBy = sale.ModifiedBy,

            DeletedOn = sale.DeletedOn,

            DeletedBy = sale.DeletedBy,

            Details = sale.Details
                .Where(d => !d.IsDeleted)
                .OrderBy(d => d.slno)
                .Select(d => new GSalDetailResponseDto
                {
                    Id = d.Id,

                    SlNo = d.slno,

                    StockCode = d.stkcode,

                    StockName = d.stkname,

                    Description = d.description,

                    UnitCode = d.unitcode,

                    UnitName = d.unitname,

                    Qty = d.qty,

                    Rate = d.rate,

                    Amount = d.amount,

                    TaxableAmount = d.taxableamt,

                    TaxRate = d.taxrate,

                    TaxAmount = d.taxamt
                })
                .ToList()
        };
    }
}



