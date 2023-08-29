using DataObjects;
using Entities;
using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS.Enemys;
using InfiniteCreativity.Services.GameNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS
{
    public class HexTileDataObject
    {
        public Guid Id { get; set; }
        public MapDataObject MapDataObject { get; set; }
        [NotMapped]
        public GameMapAccessor MapAccessor { get; set; }
        public int RowIdx { get; set; }
        public int ColIdx { get; set; }
        public TileContent TileContent { get; set; } = TileContent.Empty;
        public bool IsDiscovered { get; set; }
        public bool ReservedForPath { get; set; }
        public EntityBaseDataObject? DetailEntity { get; set; }
        public Enemy? Enemy { get; set; }

        public List<HexTileDataObject> GetNeighbours()
        {
            var neighbours = new List<HexTileDataObject>();
            var row = MapAccessor.HexTiles[RowIdx];
            var topRow = RowIdx > 0 ? MapAccessor.HexTiles[RowIdx - 1] : null;
            var bottomRow = RowIdx < MapAccessor.HexTiles.Count - 1 ? MapAccessor.HexTiles[RowIdx + 1] : null;
            if (ColIdx > 0)
            {
                neighbours.Add(row[ColIdx - 1]);
            }
            if (ColIdx < row.Count - 1)
            {
                neighbours.Add(row[ColIdx + 1]);
            }
            var offset = 1 - RowIdx % 2;
            var leftColIdx = ColIdx - offset;
            var rightColIdx = ColIdx + (1 - offset);
            if (topRow != null && leftColIdx >= 0)
            {
                neighbours.Add(topRow[leftColIdx]);
            }
            if (topRow != null && rightColIdx < topRow.Count)
            {
                neighbours.Add(topRow[rightColIdx]);
            }
            if (bottomRow != null && leftColIdx >= 0)
            {
                neighbours.Add(bottomRow[leftColIdx]);
            }
            if (bottomRow != null && rightColIdx < bottomRow.Count)
            {
                neighbours.Add(bottomRow[rightColIdx]);
            }
            return neighbours;
        }

        public bool IsWater()
        {
            return TileContent == TileContent.Water;
        }
    }
}
