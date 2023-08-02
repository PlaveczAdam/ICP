using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models
{
    public class Equippable:Item
    {
        public int EquipCount { get; set; }
        [NotMapped]
        public bool IsEquipped => EquipCount > 0;
    }
}
