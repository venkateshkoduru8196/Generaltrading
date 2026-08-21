namespace INVENTORYAPP.Features.UserManagement.DTOs;

public class PagedUserResponseDto
{
    //====================================================
    // DATA
    //====================================================

    public List<UserResponseDto> Items { get; set; }
        = new();


    //====================================================
    // PAGINATION
    //====================================================

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }
}