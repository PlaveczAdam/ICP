using AutoMapper;
using InfiniteCreativity.DTO.Game;


namespace InfiniteCreativity.Services.GameNS
{
    public interface IGameService
    {
        public Task EndGame();
        public Task<ShowGameMapDTO> GetMap();
        public Task<ShowGameTurnDTO> GetTurn();
        public Task<ShowGameTurnDTO> ProgressTurn();
        public Task StartGame(CreateGameDTO createGameDTO);
        Task<ShowWalkResultDTO> WalkPlayerRoute(CreatePlayerRouteDTO playerRoute);
    }
}