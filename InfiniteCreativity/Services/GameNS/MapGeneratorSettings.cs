using DTOs.Enums.GameNS;
using DTOs.Game;

namespace InfiniteCreativity.Services.GameNS
{
    public class MapGeneratorSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public float LandBias { get; set; }
        public float WaterFrequency { get; set; }
        public float TreeToLandRatio { get; set; }
        public int TreeBatchMax { get; set; }
        public List<MapFragmentPresetDTO> MapFragmentPresets { get; set; } = new List<MapFragmentPresetDTO>();
    }

    public class MapGeneratorPresets
    {
        private MapPresetFragments _mapPresetFragments;

        public MapGeneratorPresets(MapPresetFragments mapPresetFragments)
        {
            _mapPresetFragments = mapPresetFragments;
            Preset1 = new MapGeneratorSettings
            {
                Rows = 25,
                Columns = 25,
                LandBias = 0.6f,
                WaterFrequency = 0.05f,
                TreeToLandRatio = 0.25f,
                TreeBatchMax = 10,
                MapFragmentPresets = new List<MapFragmentPresetDTO> {
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                }
            };
            Preset2 = new MapGeneratorSettings
            {
                Rows = 5,
                Columns = 5,
                LandBias = 0.6f,
                WaterFrequency = 0.05f,
                TreeToLandRatio = 0.25f,
                TreeBatchMax = 10,
                MapFragmentPresets = new List<MapFragmentPresetDTO> {
                _mapPresetFragments.Preset1,
                }
            };
            Preset3 = new MapGeneratorSettings
            {
                Rows = 10,
                Columns = 10,
                LandBias = 0.6f,
                WaterFrequency = 0.05f,
                TreeToLandRatio = 0.25f,
                TreeBatchMax = 10,
                MapFragmentPresets = new List<MapFragmentPresetDTO> {
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                _mapPresetFragments.Preset2,
                _mapPresetFragments.Preset3,
                }
            };
        }

        public MapGeneratorSettings Preset1;
        public MapGeneratorSettings Preset2;
        public MapGeneratorSettings Preset3;

        public MapGeneratorSettings GetPresetByMapType(MapType maptype)
        { 
            switch (maptype)
            {
                case MapType.Preset1:
                    return Preset1;
                case MapType.Preset2: 
                    return Preset2;
                case MapType.Preset3:
                    return Preset3;
                default: 
                    throw new InvalidOperationException();
            }
        }
    }

}
