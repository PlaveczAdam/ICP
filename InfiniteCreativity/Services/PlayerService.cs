using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Repositorys;
using System.Security.Claims;

namespace InfiniteCreativity.Services
{
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;

        public PlayerService(
            IPlayerRepository playerRepository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor
        )
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task CreatePlayer(CreatePlayerDTO newPlayer)
        {
            await _playerRepository.CreatePlayer(_mapper.Map<Player>(newPlayer));
        }

        public async Task<int> GetPlayerIdIfValid(LoginPlayerDTO player)
        {
            var p = await _playerRepository.GetPlayer(player.Name, player.Password);
            return p.Id;
        }

        public async Task<Player> GetCurrentPlayer()
        {
            var userId = int.Parse(
                _contextAccessor.HttpContext!.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!
                    .Value
            );
            var user = await _playerRepository.GetPlayerById(userId);
            return user;
        }
    }
}
