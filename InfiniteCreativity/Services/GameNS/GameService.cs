using InfiniteCreativity.Data;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Services.CoreNS;
using Map;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services.GameNS
{
    public class GameService : IGameService
    {
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private IPlayerService _playerService;

        public GameService(InfiniteCreativityContext context, ICharacterService characterService, IPlayerService playerService)
        {
            _context = context;
            _characterService = characterService;
            _playerService = playerService;
        }

        public async  Task StartGame(CreateGameDTO createGameDTO)
        {
            if (createGameDTO.CharacterIds.Count() > 3 || createGameDTO.CharacterIds.Count() < 0)
            {
                throw new ArgumentException();
            }
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections:true);
            var oldMap = _context.Map.FirstOrDefault(x=>x.GConnection==currentPlayer.GConnections.First());
            if (oldMap is not null)
            { 
                _context.Remove(oldMap);
            }
            var characters = createGameDTO.CharacterIds.Select(async x => await _characterService.GetCharacterById(x, currentPlayer, withEquipment: true)).Select(x=>x.Result).ToList();
            MapGenerator generator = new();
            var map = generator.GenerateAndPlacePlayer(characters);
            map.GConnection = currentPlayer.GConnections.First();
            characters.ForEach(x => x.CurrentHealth = x.Health);
            _context.Map.Add(map);
            await _context.SaveChangesAsync();
        }
    }
}
