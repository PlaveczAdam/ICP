﻿namespace InfiniteCreativity.DTO
{
    public class ShowQuestDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Progression { get; set; }
        public bool IsDone { get; set; }
        public IEnumerable<ShowItemDTO> Rewards { get; set; }
        public double CashReward { get; set; }
        public double LevelReward { get; set; }
    }
}
