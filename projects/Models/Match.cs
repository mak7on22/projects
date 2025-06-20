namespace projects.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;
        public Guid Player1Id { get; set; }
        public User Player1 { get; set; } = null!;
        public Guid Player2Id { get; set; }
        public User Player2 { get; set; } = null!;
        public Guid? WinnerId { get; set; }
        public User? Winner { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
