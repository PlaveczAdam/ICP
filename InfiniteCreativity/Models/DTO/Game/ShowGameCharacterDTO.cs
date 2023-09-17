using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Weapon;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.DTO.Game
{
    public class ShowGameCharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public double CurrentHealth { get; set; }
        public double Health { get; set; }
        public double Defense { get; set; }
        public int Movement { get; set; }
        public double Damage { get; set; }
        public double AbilityDamage { get; set; }
        public double AbilityResource { get; set; }
        public double AbilityResourceGain { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
        public double Level { get; set; }
        public ShowHeadDTO Head { get; set; }
        public ShowShoulderDTO Shoulder { get; set; }
        public ShowChestDTO Chest { get; set; }
        public ShowHandDTO Hand { get; set; }
        public ShowLegDTO Leg { get; set; }
        public ShowBootDTO Boot { get; set; }
        public ShowWeaponDTO Weapon { get; set; }
    }
}
