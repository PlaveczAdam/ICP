using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface ICharacterService
    {
        public Task<ShowCharacterDTO> CreateCharacter(CreateCharacterDTO character);
        public Task<ShowEquipmentDTO> GetEquipment(int characterId);
        public Task<Character> GetCharacterById(int characterId, Player currentPlayer, bool withEquipment = false);
        public Task EquipEquipment(int characterId, int itemId);
        public Task UnequipItemFromAllCharacter(int itemId);
    }
}
