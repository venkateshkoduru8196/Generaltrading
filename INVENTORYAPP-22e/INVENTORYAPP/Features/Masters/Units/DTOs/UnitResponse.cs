namespace INVENTORYAPP.Features.Masters.Units.DTOs;

public class UnitResponse
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string code { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }
}