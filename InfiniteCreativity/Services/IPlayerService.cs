using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface IPlayerService
    {
        public Task CreatePlayer(CreatePlayerDTO newPlayer);
        public Task<int> GetPlayerIdIfValid(LoginPlayerDTO player);
        public Task<Player> GetCurrentPlayer(bool withInventory=false, bool withMessages=false, bool withConnections=false);
        public Task<ShowPlayerDTO> GetCurrentPlayerDTO();
        public Task<ShowWalletDTO> GetWallet();
        public Task<Player?> GetPlayerByName(string name);
        public Task<Player> GetPlayerById(int id);
    }
}
