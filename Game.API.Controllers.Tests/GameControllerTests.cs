using Game.Application.DTOs;
using Game.Application.Interfaces.Services;
using Game.Application.Requests;
using Game.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Game.API.Controllers.Tests
{
    public class GameControllerTests
    {
        private Mock<IGameService> _gameServiceMock;
        private Mock<IScoreboardService> _scoreboardServiceMock;
        private GameController _gameController;

        [SetUp]
        public void Setup()
        {
            _gameServiceMock = new Mock<IGameService>();
            _scoreboardServiceMock = new Mock<IScoreboardService>();
            _gameController = new GameController(_gameServiceMock.Object, _scoreboardServiceMock.Object);
        }

        [Test]
        public void Play_ReturnsGameResult()
        {
            // Arrange
            var gameMove = new ChoiceRequest { Id = 1 };
            var mockGame = new RockPaperScissorsGame
            {
                PlayerChoice = 1,
                ComputerChoice = 2,
                Result = "Win"
            };
            _gameServiceMock.Setup(x => x.PlayGame(gameMove.Id)).Returns(mockGame);

            // Act
            var result = _gameController.Play(gameMove) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.IsInstanceOf<GameResultDto>(result.Value);

            var gameResultDto = result.Value as GameResultDto;
            Assert.That(gameResultDto, Is.Not.Null);
            Assert.That(gameResultDto.Player, Is.EqualTo(gameMove.Id));
            Assert.That(gameResultDto.Computer, Is.EqualTo(mockGame.ComputerChoice));
            Assert.That(gameResultDto.Result, Is.EqualTo("Win"));

            //Verify
            _gameServiceMock.Verify(x => x.PlayGame(gameMove.Id), Times.Once);
        }

        [Test]
        public void GetRecentScores_ReturnsScores()
        {
            // Arrange
            var scores = new List<RockPaperScissorsGame>
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
            _scoreboardServiceMock.Setup(x => x.GetRecentScores()).Returns(scores);

            // Act
            var result = _gameController.GetScores() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.IsInstanceOf<List<GameResultDto>>(result.Value);
            var scoresList = result.Value as List<GameResultDto>;
            Assert.That(scoresList, Is.Not.Null.Or.Empty);
            Assert.That(scoresList.Count, Is.EqualTo(scores.Count));
        }

        [Test]
        public void ResetScoreboard_ResetsScores()
        {
            // Act
            _gameController.ResetScores();

            // Assert
            _scoreboardServiceMock.Verify(x => x.ResetScoreboard(), Times.Once);
        }
    }
}
