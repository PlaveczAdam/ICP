using InfiniteCreativity.Models.Enums.GameNS;
using System;

namespace DataObjects
{
    public class EntityBaseDataObject
    {
        public int Id { get; set; }
        public virtual TileContent Type => TileContent.Empty; 
    }
}