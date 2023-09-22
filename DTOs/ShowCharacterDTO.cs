using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO
{
    public class ShowCharacterDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public double Level { get; set; }
    }
}
