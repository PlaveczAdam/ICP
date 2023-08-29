using Entities;
using MoreLinq;

namespace InfiniteCreativity.Models.GameNS
{
    public class GameMapAccessor
    {
        public GameMapAccessor()
        {
            HexTiles = new();
        }

        public GameMapAccessor(MapDataObject mapDataObject)
        {
            Rows = mapDataObject.Rows;
            Columns = mapDataObject.Columns;
            HexTiles = mapDataObject.HexTiles
                .OrderBy(x => x.RowIdx)
                .ThenBy(x => x.ColIdx)
                .Chunk(Columns)
                .Select(x => x.ToList())
                .ToList();
            mapDataObject.HexTiles.ForEach(x => x.MapAccessor = this);
        }
        public HexTileDataObject? GetTileByIndex(int rowInd, int colInd)
        {
            if (rowInd < 0 || rowInd > Rows - 1)
            {
                return null;
            }
            if (colInd < 0 || colInd > Rows - 1)
            {
                return null;
            }
            return HexTiles[rowInd][colInd];
        }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<List<HexTileDataObject>> HexTiles { get; set; }
    }
}
