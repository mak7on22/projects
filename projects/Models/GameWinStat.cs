using System;

namespace projects.Models
{
    /// <summary>
    /// Tracks number of wins per user per game.
    /// </summary>
    public class GameWinStat
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public int Wins { get; set; }

        public Game Game { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
