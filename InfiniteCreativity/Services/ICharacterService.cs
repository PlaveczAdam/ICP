using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface ICharacterService
    {
        public Task<Character> CreateCharacter(CreateCharacterDTO character);
    }
}
