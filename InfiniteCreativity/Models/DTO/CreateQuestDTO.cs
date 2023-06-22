namespace InfiniteCreativity.Models.DTO
{
    public record CreateQuestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Progression { get; set; }
        public IEnumerable<ShowItemDTO> Rewards { get; set; }
    }
}
