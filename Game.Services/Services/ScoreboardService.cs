using Game.Application.Interfaces.Repository;
using Game.Application.Interfaces.Services;
using Game.Entities;

namespace Game.Core.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly IGameRepository _repository;

        public ScoreboardService(IGameRepository repository)
        {
            _repository = repository;
        }

        public List<RockPaperScissorsGame> GetRecentScores()
        {
            return _repository.GetGames().OrderByDescending(g => g.Timestamp).Take(10).ToList();
        }

        public void ResetScoreboard()
        {
            var games = _repository.GetGames();
            games.Clear();
        }
    }
}
