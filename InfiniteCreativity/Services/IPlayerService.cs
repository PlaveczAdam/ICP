using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface IPlayerService
    {
        public Task CreatePlayer(CreatePlayerDTO newPlayer);
        public Task<int> GetPlayerIdIfValid(LoginPlayerDTO player);
        public Task<Player> GetCurrentPlayer(bool withInventory=false);
        public Task<ShowPlayerDTO> GetCurrentPlayerDTO();
        public Task<ShowWalletDTO> GetWallet();
    }
}
