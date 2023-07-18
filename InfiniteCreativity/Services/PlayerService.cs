using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;

namespace InfiniteCreativity.Services
{
    public class PlayerService : IPlayerService
    {
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;
        private readonly InfiniteCreativityContext _context;
        private PasswordHasher<Player> _passwordHasher;

        public PlayerService(
            IMapper mapper,
            IHttpContextAccessor contextAccessor
,
            InfiniteCreativityContext context,
            PasswordHasher<Player> passwordHasher)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task CreatePlayer(CreatePlayerDTO newPlayer)
        {
            newPlayer.Password = _passwordHasher.HashPassword(_mapper.Map<Player>(newPlayer), newPlayer.Password);
            _context.Player.Add(_mapper.Map<Player>(newPlayer));
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetPlayerIdIfValid(LoginPlayerDTO player)
        {
            var p = await _context.Player
                .Include(x => x.Characters)
                .FirstOrDefaultAsync(x => x.Name == player.Name);
            if (p == null)
            {
                throw new UserNotFoundException();
            }
            var valid = _passwordHasher.VerifyHashedPassword(p, p.Password, player.Password);
            if (valid == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException();
            }

            return p.Id;
        }

        public async Task<Player> GetCurrentPlayer()
        {
            var userId = int.Parse(
                _contextAccessor.HttpContext!.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!
                    .Value
            );

            var user = await _context.Player.Include(x => x.Characters).FirstAsync(x => x.Id == userId); ;
            return user;
        }
    }
}
