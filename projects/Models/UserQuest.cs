using System;

namespace projects.Models
{
    /// <summary>
    /// Quest progress for a user.
    /// </summary>
    public class UserQuest
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        public Guid UserId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Quest Quest { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
