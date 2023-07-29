using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Weapon;

namespace InfiniteCreativity.Models.DTO
{
    public class ShowEquipmentDTO
    {
        public ShowHeadDTO Head { get; set; }
        public ShowShoulderDTO Shoulder { get; set; }
        public ShowChestDTO Chest { get; set; }
        public ShowHandDTO Hand { get; set; }
        public ShowLegDTO Leg { get; set; }
        public ShowBootDTO Boot { get; set; }
        public ShowWeaponDTO Weapon { get; set; }
    }
}
