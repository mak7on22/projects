using Microsoft.AspNetCore.Identity;

namespace projects.Models
{
    /// <summary>
    /// Application user account. Inherits from IdentityUser so that ASP.NET
    /// Core Identity can manage authentication.
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public Wallet Wallet { get; set; } = new();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
