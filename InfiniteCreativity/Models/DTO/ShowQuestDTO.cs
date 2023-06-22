namespace InfiniteCreativity.Models.DTO
{
    public class ShowQuestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Progression { get; set; }
        public IEnumerable<ShowItemDTO> Rewards { get; set; }
    }
}
