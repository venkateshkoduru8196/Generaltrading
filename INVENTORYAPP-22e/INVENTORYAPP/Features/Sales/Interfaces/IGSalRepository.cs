using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Sales.Interfaces;

public interface IGSalRepository
{
    //==========================================
    // CREATE
    //==========================================

    Task<GSal> AddAsync(GSal sale);

    //==========================================
    // GET BY ID
    // CompanyId + Invoice Id
    //==========================================

    Task<GSal?> GetByIdAsync(
        int companyId,
        int id);

    //==========================================
    // GET BY DOCUMENT NUMBER
    // CompanyId + DocNo
    //==========================================

    Task<GSal?> GetByDocNoAsync(
        int companyId,
        string docNo);

    //==========================================
    // GET ALL COMPANY SALES
    //==========================================

    Task<List<GSal>> GetAllAsync(
        int companyId);

    //==========================================
    // UPDATE
    //==========================================

    Task UpdateAsync(GSal sale);

    //==========================================
    // SOFT DELETE DETAILS
    //==========================================

    Task SoftDeleteDetailsAsync(
        List<GSalDet> details,
        string currentUser);

    //==========================================
    // ADD DETAIL
    //==========================================

    void AddDetail(GSalDet detail);

    //==========================================
    // SAVE
    //==========================================

    Task SaveChangesAsync();
}