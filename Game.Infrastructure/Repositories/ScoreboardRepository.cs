using Game.Application.Interfaces.Repository;
using Game.Entities;

namespace Game.Infrastructure.Repositories
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private readonly List<RockPaperScissorsGame> _games = new List<RockPaperScissorsGame>();

        public List<RockPaperScissorsGame> GetRecentScores()
        {
            return _games.OrderByDescending(g => g.Timestamp).Take(10).ToList();
        }

        public void ResetScores()
        {
            _games.Clear();
        }

        public void AddGame(RockPaperScissorsGame game)
        {
            _games.Add(game);
        }
    }
}
