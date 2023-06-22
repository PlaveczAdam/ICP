using InfiniteCreativity.Models;

namespace InfiniteCreativity.Repositorys
{
    public interface IPlayerRepository
    {
        public Task CreatePlayer(Player player);
        public Task<Player> UpdatePlayer(int playerId, Player player);
        public Task<Player> DeletePlayer(int playerId);
        public Task<Player> GetPlayer(string userName, string password);
        public Task<Player> GetPlayerById(int id);
    }
}
