using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models.CoreNS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InfiniteCreativity.Services.CoreNS
{
    public class PlayerService : IPlayerService
    {
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;
        private readonly InfiniteCreativityContext _context;
        private PasswordHasher<Player> _passwordHasher;

        private const int _starterPurse = 10;
        private const int _starterCharacterNumber = 5;
        private const int _starterQuestSlot = 2;

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
            var p = await _context.Player
                .Include(x => x.Characters)
                .FirstOrDefaultAsync(x => x.Name == newPlayer.Name);
            if (p == null)
            {
                var player = _mapper.Map<Player>(newPlayer);
                player.Money = _starterPurse;
                player.Password = _passwordHasher.HashPassword(player, player.Password);
                player.CharacterSlot = _starterCharacterNumber;
                player.QuestSlot = _starterQuestSlot;

                _context.Player.Add(player);
                await _context.SaveChangesAsync();
                return;
            }
            throw new UserAlreadyExistException();
        }

        public async Task<Guid> GetPlayerIdIfValid(LoginPlayerDTO player)
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

        public async Task<Player> GetCurrentPlayer(bool withInventory = false, bool withMessages = false, bool withFeConnections = false, bool withGConnections = false)
        {
            var userId = Guid.Parse(
                _contextAccessor.HttpContext!.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!
                    .Value
            );
            IQueryable<Player> userBase = _context.Player;
            if (withInventory)
            {
                userBase = userBase.Include(x => x.Inventory);
            }
            if (withMessages)
            {
                userBase = userBase
                    .Include(x => x.RecievedMessages)
                    .ThenInclude(x => x.Sender)
                    .Include(x => x.RecievedMessages)
                    .ThenInclude(x => x.Recipient)
                    .Include(x => x.SentMessages)
                    .ThenInclude(x => x.Sender)
                    .Include(x => x.SentMessages)
                    .ThenInclude(x => x.Recipient);
            }
            if (withFeConnections)
            {
                userBase = userBase.Include(x => x.FeConnections);
            }
            if (withGConnections)
            {
                userBase = userBase.Include(x => x.GConnections);
            }
            var user = await userBase.Include(x => x.Characters).FirstAsync(x => x.Id == userId);
            return user;
        }

        public async Task<ShowGamePlayerDTO> GetCurrentPlayerAll()
        {
            var userId = Guid.Parse(
                _contextAccessor.HttpContext!.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!
                    .Value
            );
            IQueryable<Player> userBase = _context.Player;
            var res = await userBase.Include((x) => x.Inventory).Include(x => x.Characters)
                    .ThenInclude((x) => x.Head)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Shoulder)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Chest)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Hand)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Leg)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Boot)
                    .Include(x => x.Characters)
                    .ThenInclude((x) => x.Weapon).FirstAsync(x => x.Id == userId);
            return _mapper.Map<ShowGamePlayerDTO>(res);
        }

        public async Task<ShowPlayerDTO> GetCurrentPlayerDTO()
        {
            var player = await GetCurrentPlayer();
            return _mapper.Map<ShowPlayerDTO>(player);
        }

        public async Task<ShowWalletDTO> GetWallet()
        {
            var player = await GetCurrentPlayer();
            return _mapper.Map<ShowWalletDTO>(player);
        }

        public async Task<Player?> GetPlayerByName(string name)
        {
            return await _context.Player.Include(x => x.FeConnections).FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<Player> GetPlayerById(Guid id)
        {
            return await _context.Player
                .Include(x => x.FeConnections)
                .Include(x => x.GConnections)
                .SingleAsync(x => x.Id == id);
        }
    }
}
