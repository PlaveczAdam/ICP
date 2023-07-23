using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StackSize { get; set; }
        public int? Value { get; set; }
        public ItemType ItemType { get; init; }
        public Player? Player { get; set; }

        public Item ShallowCopy()
        {
            return (Item)this.MemberwiseClone();
        }
    }
}
