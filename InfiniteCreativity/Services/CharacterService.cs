using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Weapons;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace InfiniteCreativity.Services
{
    public class CharacterService : ICharacterService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;

        private readonly InfiniteCreativityContext _context;

        public CharacterService(
            IPlayerService playerService,
            IMapper mapper
,
            InfiniteCreativityContext context)
        {
            _playerService = playerService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var newCharacter = _mapper.Map<Character>(character);

            newCharacter.Level = 1;
            newCharacter.BaseHealth = 100;

            currentPlayer.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
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
                   if(character.Head != null)
                    {
                        character.Head.IsEquipped = false;
                        character.Head = head;
                        character.Head.IsEquipped = true;
                    }
                    else
                    {
						character.Head = head;
						character.Head.IsEquipped = true;
					}
                    break;
                case Shoulder shoulder: 
                    character.Shoulder = shoulder;
					character.Shoulder.IsEquipped = true; 
                    break;
                case Chest chest: 
                    character.Chest = chest;
					character.Chest.IsEquipped = true; 
                    break;
                case Hand hand: 
                    character.Hand = hand;
					character.Hand.IsEquipped = true; 
                    break;
                case Leg leg: 
                    character.Leg = leg;
					character.Leg.IsEquipped = true; 
                    break;
                case Boot boot: 
                    character.Boot = boot;
					character.Boot.IsEquipped = true; 
                    break;
                case Weapon weapon: 
                    character.Weapon = weapon;
					character.Weapon.IsEquipped = true; 
                    break;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Character> GetCharacterById(int characterId, Player currentPlayer, bool withEquipment=false)
        {
            var character = 
                currentPlayer.Characters.FirstOrDefault(x => x.Id == characterId)
                ?? throw new UnauthorizedOperationException();
            if (!withEquipment)
            {
                return character;
            }
            if (withEquipment)
            {
                return await _context.Character
                    .Include((x) => x.Head)
                    .Include((x) => x.Shoulder)
                    .Include((x) => x.Chest)
                    .Include((x) => x.Hand)
                    .Include((x) => x.Leg)
                    .Include((x) => x.Boot)
                    .Include((x) => x.Weapon).SingleAsync((x)=>x.Id == characterId);
            }
            return character;
        }

        public async Task UnequipItemFromAllCharacter(int itemId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var item = await _context.Item.FindAsync(itemId);

            var charactersWithItem = await _context.Character
                .Include((x=>x.Head))
                .Include((x => x.Shoulder))
                .Include((x => x.Hand))
                .Include((x => x.Chest))
                .Include((x => x.Leg))
                .Include((x => x.Boot))
                .Include((x => x.Weapon))
                .Where((x) =>
                x.Player.Id == currentPlayer.Id && (
                    (x.Head!=null && x.Head.Id == itemId) ||
                    (x.Shoulder != null && x.Shoulder.Id == itemId) ||
                    (x.Hand != null && x.Hand.Id == itemId) ||
                    (x.Chest != null && x.Chest.Id == itemId) ||
                    (x.Leg != null && x.Leg.Id == itemId) ||
                    (x.Boot != null && x.Boot.Id == itemId) ||
                    (x.Weapon != null && x.Weapon.Id == itemId)
            )).ToListAsync();

            charactersWithItem.ForEach((x) => {
                switch (item) {
                    case Boot boot:
                        x.Boot = null;
                        x.Boot.IsEquipped = false;
                        break;
                    case Head head: x.Head = null;
						x.Head.IsEquipped = false; 
                        break;
                    case Shoulder shoulder: x.Shoulder = null;
						x.Shoulder.IsEquipped = false; 
                        break;
                    case Chest chest: x.Chest = null;
						x.Chest.IsEquipped = false; 
                        break;
                    case Hand hand: x.Hand = null;
						x.Hand.IsEquipped = false; 
                        break;
                    case Leg leg: x.Leg = null;
						x.Leg.IsEquipped = false; 
                        break;
                    case Weapon weapon: x.Weapon = null;
						x.Weapon.IsEquipped = false; 
                        break;
                } 
            }
            );
            await _context.SaveChangesAsync();
        }
    }
}
