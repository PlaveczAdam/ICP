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
                    character.Head = head; break;
                case Shoulder shoulder: 
                    character.Shoulder = shoulder; break;
                case Chest chest: 
                    character.Chest = chest; break;
                case Hand hand: 
                    character.Hand = hand; break;
                case Leg leg: 
                    character.Leg = leg; break;
                case Boot boot: 
                    character.Boot = boot; break;
                case Weapon weapon: 
                    character.Weapon = weapon; break;
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
    }
}
