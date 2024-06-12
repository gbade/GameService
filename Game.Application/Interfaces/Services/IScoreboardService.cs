using Game.Entities;

namespace Game.Application.Interfaces.Services
{
    public interface IScoreboardService
    {
        List<RockPaperScissorsGame> GetRecentScores();
        void ResetScoreboard();
    }
}
