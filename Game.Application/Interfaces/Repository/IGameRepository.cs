using Game.Entities;

namespace Game.Application.Interfaces.Repository
{
    public interface IGameRepository
    {
        void SaveGame(RockPaperScissorsGame game);
        List<RockPaperScissorsGame> GetGames();
    }
}
