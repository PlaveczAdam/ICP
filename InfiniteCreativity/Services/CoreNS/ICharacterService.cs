using DTOs;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface ICharacterService
    {
        public Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character);
        public Task<ShowEquipmentDTO> GetEquipment(Guid characterId);
        public Task<Character> GetCharacterById(Guid characterId, Player currentPlayer, bool withEquipment = false, bool withQuest = false, bool withSkillsSlots = false);
        public Task<ShowCharacterWithStatDTO> GetCharacterDTOById(Guid characterId);
        public Task EquipEquipment(Guid characterId, Guid itemId);
        public Task UnequipItemFromAllCharacter(Guid itemId);
        public Task UnequipEquipment(Guid characterId, Guid itemId);
        public Task EquipSkills(Guid characterId, UpdateCharacterSkillsDTO skills);
        public Task UnequipSkill(SkillHolder skillHolder);
        public Task<ShowCharacterSkillsDTO> GetCharacterSkills(Guid characterId);
    }
}
