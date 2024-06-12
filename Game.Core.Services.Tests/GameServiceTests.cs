using Moq;
using Game.Application.Interfaces.Repository;
using Game.Application.Interfaces.Services;
using Game.Entities;

namespace Game.Core.Services.Tests
{
    public class GameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IScoreboardRepository> _scoreboardRepositoryMock;
        private IGameService _gameService;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _scoreboardRepositoryMock = new Mock<IScoreboardRepository>();
            _gameService = new GameService(_gameRepositoryMock.Object, _scoreboardRepositoryMock.Object);
        }

        [Test]
        public void GetChoices_ReturnsAllChoices()
        {
            //Arrange
            var expectedMoveCount = 5;

            //Act
            var choices = _gameService.GetChoices();

            //Assert
            Assert.That(expectedMoveCount, Is.EqualTo(choices.Count));
            Assert.IsTrue(choices.Exists(c => c.Name == "Rock"));
            Assert.IsTrue(choices.Exists(c => c.Name == "Paper"));
            Assert.IsTrue(choices.Exists(c => c.Name == "Scissors"));
            Assert.IsTrue(choices.Exists(c => c.Name == "Lizard"));
            Assert.IsTrue(choices.Exists(c => c.Name == "Spock"));
        }

        [Test]
        public void GetRandomChoice_ReturnsChoice()
        {
            //Arrange
            var gameMoves = new[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" };

            //Act
            var choice = _gameService.GetRandomChoice();

            //Assert
            Assert.That(choice, Is.Not.Null);
            Assert.That(gameMoves, Does.Contain(choice.Name));
        }

        [Test]
        public void PlayGame_SavesGameAndReturnsResult()
         {
            //Arrange
            var playerChoiceId = 1;
            var result = new[] { "Win", "Lose", "Tie" };
            _gameRepositoryMock.Setup(x => x.SaveGame(It.IsAny<RockPaperScissorsGame>()));
            _scoreboardRepositoryMock.Setup(x => x.AddGame(It.IsAny<RockPaperScissorsGame>()));

            //Act
            var gameResult = _gameService.PlayGame(playerChoiceId);

            //Assert
            Assert.That(playerChoiceId, Is.EqualTo(gameResult.PlayerChoice));
            Assert.That(result, Does.Contain(gameResult.Result));

            //Verify
            _gameRepositoryMock.Verify(x => x.SaveGame(It.IsAny<RockPaperScissorsGame>()), Times.Once);
            _scoreboardRepositoryMock.Verify(x => x.AddGame(It.IsAny<RockPaperScissorsGame>()), Times.Once);
        }
    }
}