using InfiniteCreativity.Models.DTO.Game;
using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers
{
    [Authorize]
    [ApiController, Route("/api/game")]
    public class GameController:Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public Task StartGame(CreateGameDTO createGameDTO)
        {
            return _gameService.StartGame(createGameDTO);
        }
    }
}
