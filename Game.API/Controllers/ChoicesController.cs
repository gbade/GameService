using Game.Application.DTOs;
using Game.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.API.Controllers
{
    [Route("api/choices")]
    [ApiController]
    public class ChoicesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public ChoicesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ChoiceDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetChoices()
        {
            var choices = _gameService.GetChoices().Select(c => new ChoiceDto { Id = c.Id, Name = c.Name }).ToList();
            return Ok(choices);
        }

        [HttpGet("choice")]
        [ProducesResponseType(typeof(ChoiceDto), (int)HttpStatusCode.OK)]
        public IActionResult GetRandomChoice()
        {
            var choice = _gameService.GetRandomChoice();
            return Ok(new ChoiceDto { Id = choice.Id, Name = choice.Name });
        }
    }
}
