using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.Weapons
{
    public class Weapon : Equippable
    {
        public WeaponType WeaponType { get; set; }
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public double Range { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
    }
}
