using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers
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
    }
}
