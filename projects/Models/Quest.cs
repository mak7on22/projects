using System;

namespace projects.Models
{
    /// <summary>
    /// Daily or weekly quest definition.
    /// </summary>
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int RewardCoins { get; set; }
        public QuestFrequency Frequency { get; set; }

        public ICollection<UserQuest> UserQuests { get; set; } = new List<UserQuest>();
    }

    public enum QuestFrequency
    {
        Daily,
        Weekly
    }
}
