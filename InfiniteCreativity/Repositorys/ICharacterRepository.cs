using InfiniteCreativity.Models;

namespace InfiniteCreativity.Repositorys
{
    public interface ICharacterRepository
    {
        public Task<Character> CreateCharacter(Character character, Player player);
        public Task<Character> UpdateCharacter(int characterId, Character character);
        public Task<Character> DeleteCharacter(int characterId);
    }
}
