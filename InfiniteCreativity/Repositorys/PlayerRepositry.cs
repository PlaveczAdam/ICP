using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Repositorys
{
    public class PlayerRepositry : IPlayerRepository
    {
        private readonly InfiniteCreativityContext _context;
        private PasswordHasher<Player> _passwordHasher;

        public PlayerRepositry(
            InfiniteCreativityContext context,
            Microsoft.AspNetCore.Identity.PasswordHasher<Player> passwordHasher
        )
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task CreatePlayer(Player player)
        {
            player.Password = _passwordHasher.HashPassword(player, player.Password);
            _context.Player.Add(player);
            await _context.SaveChangesAsync();
        }

        public Task<Player> DeletePlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayer(int playerId, Player player)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> GetPlayer(string playerName, string password)
        {
            var p = await _context.Player
                .Include(x => x.Characters)
                .FirstOrDefaultAsync(x => x.Name == playerName);
            if (p == null)
            {
                throw new UserNotFoundException();
            }
            var valid = _passwordHasher.VerifyHashedPassword(p, p.Password, password);
            if (valid == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException();
            }
            return p;
        }

        public Task<Player> GetPlayerById(int id)
        {
            return _context.Player.Include(x => x.Characters).FirstAsync(x => x.Id == id);
        }
    }
}
