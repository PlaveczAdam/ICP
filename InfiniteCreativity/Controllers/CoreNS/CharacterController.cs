﻿using DTOs;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;
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
        public async Task<ShowEquipmentDTO> GetEquipment(Guid characterId)
        {
            return await _characterService.GetEquipment(characterId);
        }
        [HttpGet, Route("{characterId}")]
        public async Task<ShowCharacterWithStatDTO> GetCharacterDTOById(Guid characterId)
        {
            return await _characterService.GetCharacterDTOById(characterId);
        }
        [HttpPut, Route("equipment/{characterId}/{itemId}")]
        public async Task EquipEquipment(Guid characterId, Guid itemId)
        {
            await _characterService.EquipEquipment(characterId, itemId);
        }

        [HttpPut, Route("unequip/{characterId}/{itemId}")]
        public async Task UnequipEquipment(Guid characterId, Guid itemId)
        {
            await _characterService.UnequipEquipment(characterId, itemId);
        }

        [HttpPut, Route("skills/{characterId}")]
        public async Task EquipSkills(Guid characterId, UpdateCharacterSkillsDTO skills)
        {
            await _characterService.EquipSkills(characterId, skills);
        }

        [HttpGet, Route("skills/{characterId}")]
        public async Task<ShowCharacterSkillsDTO> GetCharacterSkills(Guid characterId)
        {
            return await _characterService.GetCharacterSkills(characterId);
        }
    }
}
