using Entities;

namespace InfiniteCreativity.Models.GameNS
{
    public class GameMapAccessor
    {
        public GameMapAccessor(MapDataObject mapDataObject)
        {
            Rows = mapDataObject.Rows;
            Columns = mapDataObject.Columns;
            HexTiles = mapDataObject.HexTiles
                .OrderBy(x => x.RowIdx)
                .ThenBy(x=>x.ColIdx)
                .Chunk(Columns)
                .Select(x=>x.ToList())
                .ToList();
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<List<HexTileDataObject>> HexTiles { get; set; }
    }
}
