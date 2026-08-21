namespace INVENTORYAPP.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRevoked { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }
    }
}