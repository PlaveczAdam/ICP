using InfiniteCreativity.Models.Enums.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class MapFragmentPresetTileDTO
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public TileContent Content { get; set; }
    }

    public class MapFragmentPresetDTO
    { 
        public List<List<MapFragmentPresetTileDTO>> Map { get; set; }
    }
}
