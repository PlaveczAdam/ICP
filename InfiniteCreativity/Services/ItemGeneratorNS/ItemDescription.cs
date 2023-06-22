using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public record ItemDescription
    {
        /*public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public int StackSize { get; set; }
        public ItemType ItemType { get; init; }
        public WeaponType WeaponType { get; set; }*/
        public Item Item { get; set; }
    }
}
