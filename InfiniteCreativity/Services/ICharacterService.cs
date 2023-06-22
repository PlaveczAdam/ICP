using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface ICharacterService
    {
        public Task CreateCharacter(CreateCharacterDTO character);
    }
}
