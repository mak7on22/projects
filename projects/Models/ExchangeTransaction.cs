using System;

namespace projects.Models
{
    /// <summary>
    /// Records conversion of regular coins to premium coins.
    /// </summary>
    public class ExchangeTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal CoinsSpent { get; set; }
        public decimal PremiumReceived { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
