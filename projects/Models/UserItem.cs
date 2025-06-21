using System;

namespace projects.Models
{
    public class UserItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime PurchasedAt { get; set; }

        public User User { get; set; } = null!;
        public Item Item { get; set; } = null!;
    }
}
