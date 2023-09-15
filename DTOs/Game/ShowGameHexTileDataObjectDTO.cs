using InfiniteCreativity.Models.Enums.GameNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.DTO.Game
{
    public class ShowGameHexTileDataObjectDTO
    {
        public Guid Id { get; set; }
        public int RowIdx { get; set; }
        public int ColIdx { get; set; }
        public TileContent TileContent { get; set; } = TileContent.Empty;
        public bool IsDiscovered { get; set; }
        public bool ReservedForPath { get; set; }
        /*public EntityBaseDataObject? DetailEntity { get; set; }*/
        public ShowGameEnemyDTO? Enemy { get; set; }
    }
}
