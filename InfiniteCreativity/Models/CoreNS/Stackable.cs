using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Stackable : Item
    {
        public ulong Amount { get; set; } = 1;
        public StackableType StackableType { get; set; }
        public Stackable Split(ulong newStackAmount)
        {
            if (newStackAmount >= Amount)
            {
                throw new InvalidOperationException();
            }
            var newStack = ShallowCopy();
            newStack.Id = Guid.Empty;
            Amount -= newStackAmount;
            newStack.Amount = newStackAmount;
            return newStack;
        }

        public Stackable ShallowCopy()
        {
            return (Stackable)MemberwiseClone();
        }
    }
}
