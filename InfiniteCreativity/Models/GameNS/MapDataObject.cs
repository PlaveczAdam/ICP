using InfiniteCreativity.Models.GameNS;
using MoreLinq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Entities
{
    public class MapDataObject
    {
        public Guid Id { get; set; }
        public GConnection GConnection { get; set; }
        public ICollection<HexTileDataObject> HexTiles { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Guid GConnectionId { get; set; }
    }
}