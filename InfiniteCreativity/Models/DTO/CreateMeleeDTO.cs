namespace InfiniteCreativity.Models.DTO
{
    public record CreateMeleeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Value { get; set; }
        public double Damage { get; set; }
        public double Range { get; set; }
        public double AttackSpeed { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
    }
}
