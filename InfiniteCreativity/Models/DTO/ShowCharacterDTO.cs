using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Weapon;

namespace InfiniteCreativity.Models.DTO
{
    public class ShowCharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Health { get; set; }
        public double Level { get; set; }
        public int Purse { get; set; }
        public IEnumerable<ShowQuestDTO>? Quests { get; set; }
    }
}
