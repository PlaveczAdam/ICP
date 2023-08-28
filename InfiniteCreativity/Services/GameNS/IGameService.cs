using InfiniteCreativity.DTO.Game;

namespace InfiniteCreativity.Services.GameNS
{
    public interface IGameService
    {
        Task<ShowGameMapDTO> GetMap();
        Task StartGame(CreateGameDTO createGameDTO);
    }
}