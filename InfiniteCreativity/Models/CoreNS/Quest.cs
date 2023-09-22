namespace InfiniteCreativity.Models.CoreNS
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Progression { get; set; }
        public IEnumerable<Item> Rewards { get; set; }
        public double CashReward { get; set; }
        public double LevelReward { get; set; }
        public Character Character { get; set; }
        public bool IsDone { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
