namespace Game.Entities
{
    public class RockPaperScissorsGame
    {
        public int Id { get; set; }
        public int PlayerChoice { get; set; }
        public int ComputerChoice { get; set; }
        public string? Result { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
