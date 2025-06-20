namespace projects.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
