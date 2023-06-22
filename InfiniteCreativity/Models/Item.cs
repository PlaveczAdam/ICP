using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models
{
    abstract public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StackSize { get; set; }
        public int? Value { get; set; }
        public ItemType ItemType { get; init; }
        public Character? Character { get; set; }

        public Item ShallowCopy()
        {
            return (Item)this.MemberwiseClone();
        }
    }
}
