public class GSalResponseDto
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string DocNo { get; set; } = string.Empty;

    public DateTime DocDate { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }

    public List<GSalDetailResponseDto> Details { get; set; } = new();
}