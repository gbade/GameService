using Game.Application.Interfaces.Repository;
using Game.Application.Interfaces.Services;
using Game.Entities;
using Moq;

namespace Game.Core.Services.Tests
{
    public class ScoreboardServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private IScoreboardService _scoreboardService;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _scoreboardService = new ScoreboardService(_gameRepositoryMock.Object);
        }

        [Test]
        public void GetRecentScores_ReturnsMostRecentScores()
        {
            // Arrange
            var scoreCount = 3;
            var mockGames = new List<RockPaperScissorsGame>
            {
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 1, 
                    ComputerChoice = 2, 
                    Result = "Win", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-1) 
                },
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 2, 
                    ComputerChoice = 3, 
                    Result = "Lose", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-2) 
                },
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 3, 
                    ComputerChoice = 4, 
                    Result = "Tie", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-3) 
                }
            };
            _gameRepositoryMock.Setup(x => x.GetGames()).Returns(mockGames);

            // Act
            var recentScores = _scoreboardService.GetRecentScores();

            // Assert
            Assert.That(scoreCount, Is.EqualTo(recentScores.Count));
            Assert.That(recentScores[0].Result, Is.EqualTo("Win"));
            Assert.That(recentScores[1].Result, Is.EqualTo("Lose"));
            Assert.That(recentScores[2].Result, Is.EqualTo("Tie"));
        }

        [Test]
        public void GetRecentScores_ReturnsMaximumOfTenScores()
        {
            // Arrange
            var mockGames = new List<RockPaperScissorsGame>();
            for (int i = 0; i < 15; i++)
            {
                mockGames.Add(new RockPaperScissorsGame
                {
                    PlayerChoice = i % 5 + 1,
                    ComputerChoice = (i + 1) % 5 + 1,
                    Result = i % 3 == 0 ? "Win" : i % 3 == 1 ? "Lose" : "Tie",
                    Timestamp = DateTime.UtcNow.AddMinutes(-i)
                });
            }
            _gameRepositoryMock.Setup(x => x.GetGames()).Returns(mockGames);

            // Act
            var recentScores = _scoreboardService.GetRecentScores();

            // Assert
            Assert.That(recentScores.Count, Is.EqualTo(10));
        }

        [Test]
        public void ResetScoreboard_ClearsAllScores()
        {
            // Arrange
            var mockGames = new List<RockPaperScissorsGame>
            {
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 1, 
                    ComputerChoice = 2, 
                    Result = "Win", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-1) 
                },
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 2, 
                    ComputerChoice = 3, 
                    Result = "Lose", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-2) 
                },
                new RockPaperScissorsGame 
                { 
                    PlayerChoice = 3, 
                    ComputerChoice = 4, 
                    Result = "Tie", 
                    Timestamp = DateTime.UtcNow.AddMinutes(-3) 
                }
            };

            _gameRepositoryMock.Setup(x => x.GetGames()).Returns(mockGames);

            // Act
            _scoreboardService.ResetScoreboard();

            // Assert
            Assert.That(mockGames.Count, Is.EqualTo(0));
        }
    }
}
