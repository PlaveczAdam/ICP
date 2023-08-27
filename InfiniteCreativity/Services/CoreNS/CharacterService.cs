using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Weapons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace InfiniteCreativity.Services.CoreNS
{
    public class CharacterService : ICharacterService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;
        private INotificationService _notificationService;

        private readonly InfiniteCreativityContext _context;

        public CharacterService(
            IPlayerService playerService,
            IMapper mapper
,
            InfiniteCreativityContext context,
            INotificationService notificationService)
        {
            _playerService = playerService;
            _mapper = mapper;
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var numberOfCharacters = currentPlayer.Characters.Count;
            if (numberOfCharacters >= currentPlayer.CharacterSlot)
            { throw new LimitReachedException(); }

            var newCharacter = _mapper.Map<Character>(character);

            newCharacter.Level = 1;

            currentPlayer.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
            return _mapper.Map<ShowCharacterDTO>(newCharacter);
        }

        public async Task<ShowEquipmentDTO> GetEquipment(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await GetCharacterById(characterId, currentPlayer, true);
            return _mapper.Map<ShowEquipmentDTO>(character);
        }

        public async Task EquipEquipment(int characterId, int itemId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var item = await _context.Item.SingleAsync((x) => x.Id == itemId && x.Player != null && x.Player.Id == currentPlayer.Id);
            var character = await GetCharacterById(characterId, currentPlayer, true);

            switch (item)
            {
                case Head head:
                    ChangeEquipment(character, head, (x) => x.Head);
                    break;
                case Shoulder shoulder:
                    ChangeEquipment(character, shoulder, (x) => x.Shoulder);
                    break;
                case Chest chest:
                    ChangeEquipment(character, chest, (x) => x.Chest);
                    break;
                case Hand hand:
                    ChangeEquipment(character, hand, (x) => x.Hand);
                    break;
                case Leg leg:
                    ChangeEquipment(character, leg, (x) => x.Leg);
                    break;
                case Boot boot:
                    ChangeEquipment(character, boot, (x) => x.Boot);
                    break;
                case Weapon weapon:
                    ChangeEquipment(character, weapon, (x) => x.Weapon);
                    break;
            }
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
        }

        public async Task<Character> GetCharacterById(int characterId, Player currentPlayer, bool withEquipment = false, bool withQuest = false)
        {
            var character =
                currentPlayer.Characters.FirstOrDefault(x => x.Id == characterId)
                ?? throw new UnauthorizedOperationException();
            IQueryable<Character> characterEntity = _context.Character;

            if (withEquipment)
            {
                characterEntity = characterEntity
                    .Include((x) => x.Head)
                    .Include((x) => x.Shoulder)
                    .Include((x) => x.Chest)
                    .Include((x) => x.Hand)
                    .Include((x) => x.Leg)
                    .Include((x) => x.Boot)
                    .Include((x) => x.Weapon);
            }
            if (withQuest)
            {
                characterEntity = characterEntity.Include((x) => x.Quests);
            }
            return await characterEntity.SingleAsync((x) => x.Id == characterId);
        }

        public async Task<ShowCharacterWithStatDTO> GetCharacterDTOById(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            return _mapper.Map<ShowCharacterWithStatDTO>(await GetCharacterById(characterId, currentPlayer, withEquipment: true));
        }

        public async Task UnequipItemFromAllCharacter(int itemId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var item = await _context.Item.FindAsync(itemId);

            var charactersWithItem = await _context.Character
                .Include(x => x.Head)
                .Include(x => x.Shoulder)
                .Include(x => x.Hand)
                .Include(x => x.Chest)
                .Include(x => x.Leg)
                .Include(x => x.Boot)
                .Include(x => x.Weapon)
                .Where((x) =>
                x.Player.Id == currentPlayer.Id && (
                    x.Head != null && x.Head.Id == itemId ||
                    x.Shoulder != null && x.Shoulder.Id == itemId ||
                    x.Hand != null && x.Hand.Id == itemId ||
                    x.Chest != null && x.Chest.Id == itemId ||
                    x.Leg != null && x.Leg.Id == itemId ||
                    x.Boot != null && x.Boot.Id == itemId ||
                    x.Weapon != null && x.Weapon.Id == itemId
            )).ToListAsync();

            charactersWithItem.ForEach((x) =>
            {
                switch (item)
                {
                    case Boot boot:
                        ChangeEquipment(x, null, (y) => y.Boot);
                        break;
                    case Head head:
                        ChangeEquipment(x, null, (y) => y.Head);
                        break;
                    case Shoulder shoulder:
                        ChangeEquipment(x, null, (y) => y.Shoulder);
                        break;
                    case Chest chest:
                        ChangeEquipment(x, null, (y) => y.Chest);
                        break;
                    case Hand hand:
                        ChangeEquipment(x, null, (y) => y.Hand);
                        break;
                    case Leg leg:
                        ChangeEquipment(x, null, (y) => y.Leg);
                        break;
                    case Weapon weapon:
                        ChangeEquipment(x, null, (y) => y.Weapon);
                        break;
                }
            }
            );
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
        }

        public async Task UnequipEquipment(int characterId, int itemId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var item = await _context.Item.FindAsync(itemId);

            var characterWithItem = await GetCharacterById(characterId, currentPlayer, true);

            switch (item)
            {
                case Boot boot:
                    ChangeEquipment(characterWithItem, null, (y) => y.Boot);
                    break;
                case Head head:
                    ChangeEquipment(characterWithItem, null, (y) => y.Head);
                    break;
                case Shoulder shoulder:
                    ChangeEquipment(characterWithItem, null, (y) => y.Shoulder);
                    break;
                case Chest chest:
                    ChangeEquipment(characterWithItem, null, (y) => y.Chest);
                    break;
                case Hand hand:
                    ChangeEquipment(characterWithItem, null, (y) => y.Hand);
                    break;
                case Leg leg:
                    ChangeEquipment(characterWithItem, null, (y) => y.Leg);
                    break;
                case Weapon weapon:
                    ChangeEquipment(characterWithItem, null, (y) => y.Weapon);
                    break;
            }
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
        }

        private void ChangeEquipment(Character character, Equippable? changeTo, Expression<Func<Character, Equippable?>> selector)
        {
            var func = selector.Compile();
            var old = func(character);
            if (old is not null)
            {
                old.EquipCount--;
            }

            if (changeTo is not null)
            {
                changeTo.EquipCount++;
            }
            if (selector.Body is MemberExpression memberExpression)
            {
                var propertyInfo = (PropertyInfo)memberExpression.Member;
                propertyInfo.SetValue(character, changeTo);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
