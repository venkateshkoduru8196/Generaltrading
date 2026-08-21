namespace INVENTORYAPP.Features.UserManagement.DTOs;

public class UserListRequestDto
{
    //====================================================
    // SEARCH
    //====================================================

    public string? Search { get; set; }


    //====================================================
    // STATUS FILTER
    //====================================================

    public bool? IsActive { get; set; }


    //====================================================
    // PAGINATION
    //====================================================

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}