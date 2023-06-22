namespace InfiniteCreativity.Models.DTO
{
    public class ShowWeaponDTO : ShowItemDTO
    {
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public double Range { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
    }
}
