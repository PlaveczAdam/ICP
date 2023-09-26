﻿using DTOs.Game;
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

        [HttpGet, Route("turn")]
        public Task<ShowGameTurnDTO> GetTurn()
        {
            return _gameService.GetTurn();
        }
        [HttpPut, Route("turn")]
        public Task<ShowGameTurnDTO> ProgressTurn()
        {
            return _gameService.ProgressTurn();
        }
        [HttpDelete]
        public Task EndGame()
        {
            return _gameService.EndGame();
        }
        [HttpPut, Route("walk")]
        public Task<ShowWalkResultDTO> WalkPlayerRoute(CreatePlayerRouteDTO playerRoute)
        {
            return _gameService.WalkPlayerRoute(playerRoute);
        }
        [HttpPost, Route("battle")]
        public Task<ShowBattleStateDTO> StartBattle()
        {
            return _gameService.StartBattle();
        }
        [HttpGet, Route("battle")]
        public Task<ShowBattleStateDTO> GetCurrentBattleState()
        {
            return _gameService.GetCurrentBattleState();
        }

        [HttpPost, Route("battle/turn")]
        public Task<ShowBattleStateDTO> MakePlayerTurn([FromBody] CreatePlayerActionDTO playerAction)
        {
            return _gameService.MakePlayerTurn(playerAction);
        }


    }
}
