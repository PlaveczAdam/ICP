using InfiniteCreativity.Models.GameNS;

namespace InfiniteCreativity.Services.GameNS
{
    public class MapRaw
    {
        public List<List<HexTileDataObject>> HexTiles { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
