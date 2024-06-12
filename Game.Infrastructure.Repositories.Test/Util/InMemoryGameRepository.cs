using Game.Entities;
using Game.Application.Interfaces.Repository;

namespace Game.Infrastructure.Repositories.Test.Util
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly List<RockPaperScissorsGame> _games = new();

        public void SaveGame(RockPaperScissorsGame game)
        {
            _games.Add(game);
        }

        public List<RockPaperScissorsGame> GetGames()
        {
            return _games.ToList();
        }
    }
}
