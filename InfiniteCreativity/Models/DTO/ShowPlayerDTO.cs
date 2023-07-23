namespace InfiniteCreativity.Models.DTO
{
    public class ShowPlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ShowItemDTO>? Inventory { get; set; }
        public  IEnumerable<ShowCharacterDTO> Characters { get; set; }
    }
}
