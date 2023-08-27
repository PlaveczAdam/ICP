using InfiniteCreativity.DTO.Game;

namespace InfiniteCreativity.Services.GameNS
{
    public interface IGameService
    {
        Task StartGame(CreateGameDTO createGameDTO);
    }
}