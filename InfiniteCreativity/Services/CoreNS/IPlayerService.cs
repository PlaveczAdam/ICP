using InfiniteCreativity.DTO;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IPlayerService
    {
        public Task CreatePlayer(CreatePlayerDTO newPlayer);
        public Task<Guid> GetPlayerIdIfValid(LoginPlayerDTO player);
        public Task<Player> GetCurrentPlayer(bool withInventory = false, bool withMessages = false, bool withFeConnections = false, bool withGConnections = false);
        public Task<ShowGamePlayerDTO> GetCurrentPlayerAll();
        public Task<ShowPlayerDTO> GetCurrentPlayerDTO();
        public Task<ShowWalletDTO> GetWallet();
        public Task<Player?> GetPlayerByName(string name);
        public Task<Player> GetPlayerById(Guid id);
    }
}
