using DataObjects;
using InfiniteCreativity.Models.Enums.GameNS;

namespace Entities
{
    public class CityEntityDataObject : EntityBaseDataObject
    {
        public string Name;
        public override TileContent Type => TileContent.City;
    }
}