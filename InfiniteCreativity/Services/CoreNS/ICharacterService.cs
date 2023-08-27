using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface ICharacterService
    {
        public Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character);
        public Task<ShowEquipmentDTO> GetEquipment(int characterId);
        public Task<Character> GetCharacterById(int characterId, Player currentPlayer, bool withEquipment = false, bool withQuest = false);
        public Task<ShowCharacterWithStatDTO> GetCharacterDTOById(int characterId);
        public Task EquipEquipment(int characterId, int itemId);
        public Task UnequipItemFromAllCharacter(int itemId);
        public Task UnequipEquipment(int characterId, int itemId);
    }
}
