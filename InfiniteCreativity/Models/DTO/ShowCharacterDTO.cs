namespace InfiniteCreativity.Models.DTO
{
    public class ShowCharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Purse { get; set; }
        public IEnumerable<ShowItemDTO>? Inventory { get; set; }
        public IEnumerable<ShowQuestDTO>? Quests { get; set; }
    }
}
