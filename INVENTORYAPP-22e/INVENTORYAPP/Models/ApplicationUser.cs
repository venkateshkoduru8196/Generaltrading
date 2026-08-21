using Microsoft.AspNetCore.Identity;

namespace INVENTORYAPP.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    // ==========================================
    // Company (Multi-Tenant)
    // ==========================================

    public int? CompanyId { get; set; }

    public Company? Company { get; set; }

    // ==========================================
    // Status
    // ==========================================

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    // ==========================================
    // Audit Fields
    // ==========================================

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public string CreatedBy { get; set; } = "SYSTEM";

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }

    // ==========================================
    // Login Information
    // ==========================================

    public DateTime? LastLoginOn { get; set; }

    public string? EditPasswordHash { get; set; }

    // ==========================================
    // Navigation
    // ==========================================

    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}