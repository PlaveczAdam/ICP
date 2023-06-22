using AutoMapper;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Repositorys;

namespace InfiniteCreativity.Services
{
    public class CharacterService : ICharacterService
    {
        private ICharacterRepository _characterRepository;
        private IPlayerService _playerService;
        private IMapper _mapper;

        private const int _starterPurse = 10;

        public CharacterService(
            ICharacterRepository characterRepository,
            IPlayerService playerService,
            IMapper mapper
        )
        {
            _characterRepository = characterRepository;
            _playerService = playerService;
            _mapper = mapper;
        }

        public async Task CreateCharacter(CreateCharacterDTO character)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var newCharacter = _mapper.Map<Character>(character);
            newCharacter.Purse = _starterPurse;
            var pc = await _characterRepository.CreateCharacter(newCharacter, currentPlayer);
        }
    }
}
