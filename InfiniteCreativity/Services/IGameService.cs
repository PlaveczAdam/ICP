using InfiniteCreativity.Models.DTO.Game;

namespace InfiniteCreativity.Services
{
    public interface IGameService
    {
        Task StartGame(CreateGameDTO createGameDTO);
    }
}