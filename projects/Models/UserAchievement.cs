using System;

namespace projects.Models
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public DateTime AchievedAt { get; set; }
        public User User { get; set; } = null!;
        public Achievement Achievement { get; set; } = null!;
    }
}
