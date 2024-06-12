using Game.Entities;

namespace Game.Application.Interfaces.Services
{
    public interface IGameService
    {
        Choice GetRandomChoice();
        RockPaperScissorsGame PlayGame(int playerChoiceId);
        List<Choice> GetChoices();
    }
}
