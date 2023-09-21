using DTOs;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface ICharacterService
    {
        public Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character);
        public Task<ShowEquipmentDTO> GetEquipment(int characterId);
        public Task<Character> GetCharacterById(int characterId, Player currentPlayer, bool withEquipment = false, bool withQuest = false, bool withSkillsSlots = false);
        public Task<ShowCharacterWithStatDTO> GetCharacterDTOById(int characterId);
        public Task EquipEquipment(int characterId, Guid itemId);
        public Task UnequipItemFromAllCharacter(Guid itemId);
        public Task UnequipEquipment(int characterId, Guid itemId);
        public Task EquipSkills(int characterId, UpdateCharacterSkillsDTO skills);
        public Task UnequipSkill(SkillHolder skillHolder);
        public Task<ShowCharacterSkillsDTO> GetCharacterSkills(int characterId);
    }
}
