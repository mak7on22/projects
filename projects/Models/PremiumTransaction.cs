using System;

namespace projects.Models
{
    /// <summary>
    /// Records purchase of premium currency.
    /// </summary>
    public class PremiumTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
