using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Weapon;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.DTO
{
    public class ShowCharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public double Level { get; set; }
    }
}
