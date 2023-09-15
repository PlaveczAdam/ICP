using InfiniteCreativity.Models.GameNS;

namespace InfiniteCreativity.DTO.Game
{
    public class ShowGameMapDTO
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<List<ShowGameHexTileDataObjectDTO>> HexTiles { get; set; }
    }
}
