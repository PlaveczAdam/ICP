using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.DTO
{
    public record CreateCharacterDTO
    {
        public string Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
    }
}
