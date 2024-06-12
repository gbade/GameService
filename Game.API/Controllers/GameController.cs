using Game.Application.DTOs;
using Game.Application.Interfaces.Services;
using Game.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.API.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IScoreboardService _scoreboardService;

        public GameController(IGameService gameService, IScoreboardService scoreboardService)
        {
            _gameService = gameService;
            _scoreboardService = scoreboardService;
        }

        [HttpPost("play")]
        [ProducesResponseType(typeof(GameResultDto), (int)HttpStatusCode.OK)]
        public IActionResult Play([FromBody] ChoiceRequest playerChoice)
        {
            var gameResult = _gameService.PlayGame(playerChoice.Id);
            return Ok(new GameResultDto
            {
                Result = gameResult.Result,
                Player = gameResult.PlayerChoice,
                Computer = gameResult.ComputerChoice,
                Timestamp = gameResult.Timestamp
            });
        }

        [HttpGet("scores")]
        [ProducesResponseType(typeof(List<GameResultDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetScores()
        {
            var scores = _scoreboardService.GetRecentScores().Select(s => new GameResultDto
            {
                Result = s.Result,
                Player = s.PlayerChoice,
                Computer = s.ComputerChoice,
                Timestamp = s.Timestamp
            }).ToList();
            return Ok(scores);
        }

        [HttpPost("scores/reset")]
        public IActionResult ResetScores()
        {
            _scoreboardService.ResetScoreboard();
            return Ok();
        }
    }
}
