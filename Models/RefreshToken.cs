using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string JwtTokenId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Refresh_Token { get; set; }
        public bool IsValid { get; set; }

    }
}