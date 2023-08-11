using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models
{
    public class Stackable:Item
    {
        public int Amount { get; set; } = 1;
        public StackableType StackableType { get; set; }
    }
}
