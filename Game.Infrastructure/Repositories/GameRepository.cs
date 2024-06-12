using Game.Application.Interfaces.Repository;
using Game.Entities;

namespace Game.Infrastructure.Repositories
{
    public  class GameRepository : IGameRepository
    {
        private readonly List<RockPaperScissorsGame> _games = new List<RockPaperScissorsGame>();

        public void SaveGame(RockPaperScissorsGame game)
        {
            _games.Add(game);
        }

        public List<RockPaperScissorsGame> GetGames() => _games;
    }
}
