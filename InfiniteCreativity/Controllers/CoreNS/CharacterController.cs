using InfiniteCreativity.DTO;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [Authorize]
    [ApiController, Route("/api/character")]
    public class CharacterController : Controller
    {
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpPost]
        public async Task CreateCharacter([FromBody] CreateCharacterDTO newCharacter)
        {
            await _characterService.CreateCharacter(newCharacter);
        }

        [HttpGet, Route("equipment/{characterId}")]
        public async Task<ShowEquipmentDTO> GetEquipment(int characterId)
        {
            return await _characterService.GetEquipment(characterId);
        }
        [HttpGet, Route("{characterId}")]
        public async Task<ShowCharacterWithStatDTO> GetCharacterDTOById(int characterId)
        {
            return await _characterService.GetCharacterDTOById(characterId);
        }
        [HttpPut, Route("equipment/{characterId}/{itemId}")]
        public async Task EquipEquipment(int characterId, Guid itemId)
        {
            await _characterService.EquipEquipment(characterId, itemId);
        }

        [HttpPut, Route("unequip/{characterId}/{itemId}")]
        public async Task UnequipEquipment(int characterId, Guid itemId)
        {
            await _characterService.UnequipEquipment(characterId, itemId);
        }
    }
}
