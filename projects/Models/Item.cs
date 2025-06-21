using System;
using System.Collections.Generic;

namespace projects.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsPremium { get; set; }
        public ICollection<UserItem> UserItems { get; set; } = new List<UserItem>();
    }
}
