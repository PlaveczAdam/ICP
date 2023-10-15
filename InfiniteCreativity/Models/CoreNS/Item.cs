using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public abstract class Item
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RarityType Rarity { get; set; }
        public int? Value { get; set; }
        public ItemType ItemType { get; init; }
        public Player? Player { get; set; }

        public Item ShallowCopy()
        {
            return (Item)MemberwiseClone();
        }
    }
}
