using AutoMapper;
using DTOs.Game;
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
        public Task<ShowWalkResultDTO> WalkPlayerRoute(CreatePlayerRouteDTO playerRoute);
        public Task<ShowBattleStateDTO> StartBattle();
        public Task<ShowBattleStateDTO> GetCurrentBattleState();
        public Task<ShowBattleStateDTO> MakePlayerTurn(CreatePlayerActionDTO playerAction);
        public Task<ShowGameTurnDTO> CutTree(CreateTreeCutDTO playerAction);
    }
}