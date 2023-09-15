using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO
{
    public record CreateCharacterDTO
    {
        public string Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
    }
}
