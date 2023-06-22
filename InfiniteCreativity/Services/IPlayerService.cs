using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface IPlayerService
    {
        public Task CreatePlayer(CreatePlayerDTO newPlayer);
        public Task<int> GetPlayerIdIfValid(LoginPlayerDTO player);
        public Task<Player> GetCurrentPlayer();
    }
}
