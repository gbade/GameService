﻿using Game.Application.Interfaces.Repository;
using Game.Entities;
using Game.Infrastructure.Repositories.Test.Util;

namespace Game.Infrastructure.Repositories.Test
{
    public class ScoreboardRepositoryTests
    {
        private IScoreboardRepository _scoreboardRepository;

        [SetUp]
        public void Setup()
        {
            _scoreboardRepository = new InMemoryScoreboardRepository();
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
            _scoreboardRepository.AddGame(game);
            var games = _scoreboardRepository.GetRecentScores();

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

            _scoreboardRepository.AddGame(game1);
            _scoreboardRepository.AddGame(game2);

            // Act
            var games = _scoreboardRepository.GetRecentScores();

            // Assert
            Assert.That(games.Count, Is.EqualTo(2));
            Assert.Contains(game1, games);
            Assert.Contains(game2, games);
        }

        [Test]
        public void ClearGames_ShouldRemoveAllGames()
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

            _scoreboardRepository.AddGame(game1);
            _scoreboardRepository.AddGame(game2);

            // Act
            _scoreboardRepository.ResetScores();
            var games = _scoreboardRepository.GetRecentScores();

            // Assert
            Assert.That(games.Count, Is.EqualTo(0));
        }
    }
}
