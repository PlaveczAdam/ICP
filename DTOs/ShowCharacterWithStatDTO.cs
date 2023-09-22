using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO
{
    public class ShowCharacterWithStatDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public double Level { get; set; }
        public double Health { get; set; }
        public double Defense { get; set; }
        public int Movement { get; set; }
        public double Damage { get; set; }
        public double AbilityDamage { get; set; }
        public double AbilityResource { get; set; }
        public double AbilityResourceGain { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
    }
}
