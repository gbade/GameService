using Game.Application.Interfaces.Repository;
using Game.Application.Interfaces.Services;
using Game.Entities;

namespace Game.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IScoreboardRepository _scoreboardRepository;
        private static readonly Random Random = new Random();
        private readonly List<Choice> _choices = new List<Choice>
        {
            new Choice { Id = 1, Name = "Rock" },
            new Choice { Id = 2, Name = "Paper" },
            new Choice { Id = 3, Name = "Scissors" },
            new Choice { Id = 4, Name = "Lizard" },
            new Choice { Id = 5, Name = "Spock" }
        };

        public GameService(IGameRepository gameRepository, IScoreboardRepository scoreboardRepository)
        {
            _gameRepository = gameRepository;
            _scoreboardRepository = scoreboardRepository;
        }

        public List<Choice> GetChoices() => _choices;

        public RockPaperScissorsGame PlayGame(int playerChoiceId)
        {
            var playerChoice = _choices.Find(c => c.Id == playerChoiceId);
            var computerChoice = GetRandomChoice();
            var result = DetermineWinner(playerChoice, computerChoice);
            var game = new RockPaperScissorsGame
            {
                PlayerChoice = playerChoice.Id,
                ComputerChoice = computerChoice.Id,
                Result = result,
                Timestamp = DateTime.UtcNow
            };

            _gameRepository.SaveGame(game);
            _scoreboardRepository.AddGame(game);

            return game;
        }

        public Choice GetRandomChoice()
        {
            return _choices[Random.Next(_choices.Count)];
        }

        private string DetermineWinner(Choice playerChoice, Choice computerChoice)
        {
            if (playerChoice.Id == computerChoice.Id)
                return "Tie";

            return playerChoice.Name switch
            {
                "Rock" => (computerChoice.Name == "Scissors" || computerChoice.Name == "Lizard") ? "Win" : "Lose",
                "Paper" => (computerChoice.Name == "Rock" || computerChoice.Name == "Spock") ? "Win" : "Lose",
                "Scissors" => (computerChoice.Name == "Paper" || computerChoice.Name == "Lizard") ? "Win" : "Lose",
                "Lizard" => (computerChoice.Name == "Spock" || computerChoice.Name == "Paper") ? "Win" : "Lose",
                "Spock" => (computerChoice.Name == "Scissors" || computerChoice.Name == "Rock") ? "Win" : "Lose",
                _ => throw new ArgumentOutOfRangeException($"{playerChoice.Name} - Invalid choice name provided.")
            };
        }
    }
}
