using Game.Application.Interfaces.Services;
using Game.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Game.API.Controllers.Tests
{
    public class ChoicesControllerTests
    {
        private Mock<IGameService> _gameServiceMock;
        private ChoicesController _choicesController;

        [SetUp]
        public void Setup()
        {
            _gameServiceMock = new Mock<IGameService>();
            _choicesController = new ChoicesController(_gameServiceMock.Object);
        }

        [Test]
        public void GetChoices_ReturnsAllChoices()
        {
            // Arrange
            var mockChoices = new List<Choice>
            {
                new Choice { Id = 1, Name = "Rock" },
                new Choice { Id = 2, Name = "Paper" },
                new Choice { Id = 3, Name = "Scissors" }
            };
            _gameServiceMock.Setup(x => x.GetChoices()).Returns(mockChoices);

            // Act
            var result = _choicesController.GetChoices() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.IsInstanceOf<List<Application.DTOs.ChoiceDto>>(result.Value);
            var choices = result.Value as List<Application.DTOs.ChoiceDto>;
            Assert.That(choices, Is.Not.Empty);
            Assert.That(choices.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetRandomChoice_ReturnsChoice()
        {
            // Arrange
            var mockChoice = new Choice { Id = 1, Name = "Rock" };
            _gameServiceMock.Setup(x => x.GetRandomChoice()).Returns(mockChoice);

            // Act
            var result = _choicesController.GetRandomChoice() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.IsInstanceOf<Application.DTOs.ChoiceDto>(result.Value);
            var choice = result.Value as Application.DTOs.ChoiceDto;
            Assert.That(choice, Is.Not.Null);
            Assert.That(choice.Id, Is.EqualTo(1));
            Assert.That(choice.Name, Is.EqualTo("Rock"));
        }
    }
}