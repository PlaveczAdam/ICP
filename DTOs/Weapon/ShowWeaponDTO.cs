using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO.Weapon
{
    public class ShowWeaponDTO : ShowEquippableDTO
    {
        public double Damage { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}
