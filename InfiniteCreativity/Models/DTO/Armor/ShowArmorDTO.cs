using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.DTO.Armor
{
    public class ShowArmorDTO : ShowEquippableDTO
    {
        public ArmorType ArmorType { get; set; }
        public double ArmorValue { get; set; }
    }

}
