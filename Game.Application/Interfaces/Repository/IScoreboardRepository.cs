using Game.Entities;

namespace Game.Application.Interfaces.Repository
{
    public interface IScoreboardRepository
    {
        List<RockPaperScissorsGame> GetRecentScores();
        void AddGame(RockPaperScissorsGame game);
        void ResetScores();
    }
}
