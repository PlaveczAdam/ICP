using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.CoreNS.ArmorNs
{
    public class Armor : Equippable
    {
        public ArmorType ArmorType { get; set; }
        public double ArmorValue { get; set; }
        public double Health { get; set; }

    }
}
