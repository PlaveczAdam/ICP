using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Weapon;

namespace InfiniteCreativity.Models.DTO.Game
{
    public class ShowGameCharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Health { get; set; }
        public double Level { get; set; }
        public int Purse { get; set; }
        public ShowHeadDTO Head { get; set; }
        public ShowShoulderDTO Shoulder { get; set; }
        public ShowChestDTO Chest { get; set; }
        public ShowHandDTO Hand { get; set; }
        public ShowLegDTO Leg { get; set; }
        public ShowBootDTO Boot { get; set; }
        public ShowWeaponDTO Weapon { get; set; }
    }
}
