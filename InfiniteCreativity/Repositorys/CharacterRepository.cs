using InfiniteCreativity.Data;
using InfiniteCreativity.Models;

namespace InfiniteCreativity.Repositorys
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly InfiniteCreativityContext _context;

        public CharacterRepository(InfiniteCreativityContext context)
        {
            _context = context;
        }

        public async Task<Character> CreateCharacter(Character character, Player player)
        {
            player.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public Task<Character> DeleteCharacter(int characterId)
        {
            throw new NotImplementedException();
        }

        public Task<Character> UpdateCharacter(int characterId, Character character)
        {
            throw new NotImplementedException();
        }
    }
}
