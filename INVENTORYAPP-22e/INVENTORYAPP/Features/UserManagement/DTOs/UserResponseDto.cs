namespace INVENTORYAPP.Features.UserManagement.DTOs;

public class UserResponseDto
{
    //====================================================
    // USER
    //====================================================

    public string UserId { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;


    //====================================================
    // ROLE
    //====================================================

    public string RoleId { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;


    //====================================================
    // COMPANY
    //====================================================

    public int? CompanyId { get; set; }

    public string CompanyCode { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;


    //====================================================
    // STATUS
    //====================================================

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }


    //====================================================
    // AUDIT
    //====================================================

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? LastLoginOn { get; set; }
}