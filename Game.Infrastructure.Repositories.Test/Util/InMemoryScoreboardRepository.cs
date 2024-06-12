using Game.Entities;
using Game.Application.Interfaces.Repository;

namespace Game.Infrastructure.Repositories.Test.Util
{
    public class InMemoryScoreboardRepository : IScoreboardRepository
    {
        private readonly List<RockPaperScissorsGame> _games = new();

        public List<RockPaperScissorsGame> GetRecentScores()
        {
            return _games.OrderByDescending(g => g.Timestamp).Take(10).ToList();
        }

        public void AddGame(RockPaperScissorsGame game)
        {
            _games.Add(game);
        }

        public void ResetScores()
        {
            _games.Clear();
        }
    }
}
