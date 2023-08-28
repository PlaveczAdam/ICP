using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Services.GameNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.GameNS
{
    [Authorize]
    [ApiController, Route("/api/game")]
    public class GameController : Controller
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

        [HttpGet]
        public Task<ShowGameMapDTO> GetMap()
        {
            return _gameService.GetMap();
        }
    }
}
