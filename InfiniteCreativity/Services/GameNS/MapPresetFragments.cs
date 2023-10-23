using DTOs.Game;
using Newtonsoft.Json;

namespace InfiniteCreativity.Services.GameNS
{
    public class MapPresetFragments
    {
        public MapFragmentPresetDTO Preset1 = JsonConvert.DeserializeObject<MapFragmentPresetDTO>("{\"map\":[[{\"row\":0,\"col\":0,\"content\":\"tree\"},{\"row\":0,\"col\":1,\"content\":\"water\"},{\"row\":0,\"col\":2,\"content\":\"tree\"},{\"row\":0,\"col\":3,\"content\":\"empty\"}],[{\"row\":1,\"col\":0,\"content\":\"empty\"},{\"row\":1,\"col\":1,\"content\":\"water\"},{\"row\":1,\"col\":2,\"content\":\"city\"},{\"row\":1,\"col\":3,\"content\":\"empty\"}],[{\"row\":2,\"col\":0,\"content\":\"tree\"},{\"row\":2,\"col\":1,\"content\":\"water\"},{\"row\":2,\"col\":2,\"content\":\"tree\"},{\"row\":2,\"col\":3,\"content\":\"empty\"}],[{\"row\":3,\"col\":0,\"content\":\"empty\"},{\"row\":3,\"col\":1,\"content\":\"tree\"},{\"row\":3,\"col\":2,\"content\":\"tree\"},{\"row\":3,\"col\":3,\"content\":\"empty\"}]]}");
        public MapFragmentPresetDTO Preset2 = JsonConvert.DeserializeObject<MapFragmentPresetDTO>("{\"map\":[[{\"row\":0,\"col\":0,\"content\":\"city\"},{\"row\":0,\"col\":1,\"content\":\"city\"},{\"row\":0,\"col\":2,\"content\":\"city\"}],[{\"row\":1,\"col\":0,\"content\":\"city\"},{\"row\":1,\"col\":1,\"content\":\"water\"},{\"row\":1,\"col\":2,\"content\":\"city\"}],[{\"row\":2,\"col\":0,\"content\":\"city\"},{\"row\":2,\"col\":1,\"content\":\"city\"},{\"row\":2,\"col\":2,\"content\":\"city\"}]]}");
        //public MapFragmentPresetDTO Preset2 = JsonConvert.DeserializeObject<MapFragmentPresetDTO>();
    }

}
