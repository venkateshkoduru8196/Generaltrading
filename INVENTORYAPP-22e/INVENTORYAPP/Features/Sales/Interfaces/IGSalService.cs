using INVENTORYAPP.Features.Sales.DTOs;
using INVENTORYAPP.Features.Sales.Export.DTOs;

namespace INVENTORYAPP.Features.Sales.Interfaces;

public interface IGSalService
{
    //==========================================
    // CREATE
    //==========================================

    Task<GSalResponseDto> CreateAsync(
        GSalCreateRequestDto request);

    //==========================================
    // GET BY ID
    //==========================================

    Task<GSalResponseDto?> GetByIdAsync(
        int id);

    //==========================================
    // GET BY DOCUMENT NUMBER
    //==========================================

    Task<GSalResponseDto?> GetByDocNoAsync(
        string docNo);

    //==========================================
    // GET ALL
    //==========================================

    Task<List<GSalResponseDto>> GetAllAsync();

    //==========================================
    // UPDATE
    //==========================================

    Task UpdateAsync(
        int id,
        GSalCreateRequestDto request);

    //==========================================
    // DELETE
    //==========================================

    Task DeleteAsync(
        int id);

    //==========================================
    // EXPORT INVOICE
    //==========================================

    Task<SalesInvoiceExportDto?> GetInvoiceForExportAsync(
        int saleId);


}


