using DTOs.Enums.GameNS;

namespace InfiniteCreativity.DTO.Game
{
    public class CreateGameDTO
    {
        public IEnumerable<Guid> CharacterIds { get; set; }
        public MapType Maptype { get; set; }
    }
}
