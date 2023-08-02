using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.DTO.Weapon
{
    public class ShowWeaponDTO : ShowEquippableDTO
    {
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public double Range { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}
