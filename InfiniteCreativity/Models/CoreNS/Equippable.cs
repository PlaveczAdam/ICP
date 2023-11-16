using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Equippable : Item
    {
        public int EquipCount { get; set; }
        [NotMapped]
        public bool IsEquipped => EquipCount > 0;
        public string ModelName { get; set; }
    }
}
