using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace InfiniteCreativity.Services
{
    public class CharacterService : ICharacterService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;

        private readonly InfiniteCreativityContext _context;
        private const int _starterPurse = 10;

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

        public async Task<Character> CreateCharacter(CreateCharacterDTO character)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var newCharacter = _mapper.Map<Character>(character);
            newCharacter.Purse = _starterPurse;

            currentPlayer.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return newCharacter;
        }
    }
}
