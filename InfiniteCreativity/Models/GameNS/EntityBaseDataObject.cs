using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS;
using System;

namespace DataObjects
{
    public class EntityBaseDataObject
    {
        public int Id { get; set; }
        public virtual TileContent Type => TileContent.Empty; 
        public HexTileDataObject HexTileDataObject { get; set; }
        public Guid HexTileDataObjectId { get; set; }
    }
}