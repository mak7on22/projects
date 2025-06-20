namespace projects.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal PremiumBalance { get; set; }
        public User User { get; set; } = null!;
    }
}
