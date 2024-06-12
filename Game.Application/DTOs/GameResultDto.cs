namespace Game.Application.DTOs
{
    public class GameResultDto
    {
        public string? Result { get; set; }
        public int Player { get; set; }
        public int Computer { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
