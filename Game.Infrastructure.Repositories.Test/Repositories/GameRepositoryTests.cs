using Game.Application.Interfaces.Repository;
using Game.Entities;
using Game.Infrastructure.Repositories.Test.Util;

namespace Game.Infrastructure.Repositories.Test
{
    public class GameRepositoryTests
    {
        private IGameRepository _gameRepository;

        [SetUp]
        public void Setup()
        {
            _gameRepository = new InMemoryGameRepository();
        }

        [Test]
        public void AddGame_ShouldAddGameToRepository()
        {
            // Arrange
            var game = new RockPaperScissorsGame
            {
                PlayerChoice = 1,
                ComputerChoice = 2,
                Result = "Win"
            };

            // Act
            _gameRepository.SaveGame(game);
            var games = _gameRepository.GetGames();

            // Assert
            Assert.That(games.Count, Is.EqualTo(1));
            Assert.That(games[0], Is.EqualTo(game));
        }

        [Test]
        public void GetGames_ShouldReturnAllGames()
        {
            // Arrange
            var game1 = new RockPaperScissorsGame
            {
                PlayerChoice = 1,
                ComputerChoice = 2,
                Result = "Win"
            };

            var game2 = new RockPaperScissorsGame
            {
                PlayerChoice = 2,
                ComputerChoice = 3,
                Result = "Lose"
            };

            _gameRepository.SaveGame(game1);
            _gameRepository.SaveGame(game2);

            // Act
            var games = _gameRepository.GetGames();

            // Assert
            Assert.That(games.Count, Is.EqualTo(2));
            Assert.Contains(game1, games);
            Assert.Contains(game2, games);
        }
    }
}
