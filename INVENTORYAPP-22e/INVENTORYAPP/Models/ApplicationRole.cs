using Microsoft.AspNetCore.Identity;

namespace INVENTORYAPP.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}