using Entities;
using Extensions;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Services.GameNS;

namespace Map
{
    public class MapGenerator
    {
        public int rows = 25;
        public int columns = 25;
        private float landBias = 0.6f;
        private float waterFrequency = 0.05f;
        private float treeToLandRatio = 0.25f;
        private int treeBatchMax = 10;
        private string mapOverride = "";
        private HexTileDataObject startTile;
        private Random _rnd = new Random();

        public MapDataObject GenerateAndPlacePlayer(List<Character> playersDataObjects)
        {
            if (mapOverride.Length > 0)
            {
                var md = ProcessOverride();
                InitPlayers(playersDataObjects);
                return md;
            }
            var mapData = new GameMapAccessor()
            {
                Columns = columns,
                Rows = rows,
                HexTiles = new(),
            };

            for (var row = 0; row < rows; row++)
            {
                var hexTiles = new List<HexTileDataObject>();
                for (var column = 0; column < columns; column++)
                {
                    var hexTile = new HexTileDataObject()
                    {
                        ColIdx = column,
                        RowIdx = row,
                        IsDiscovered = false,
                        TileContent = TileContent.Water,
                        MapAccessor = mapData
                    };
                    hexTiles.Add(hexTile);
                }

                mapData.HexTiles.Add(hexTiles);
            }

            PopulateMap(mapData);
            InitPlayers(playersDataObjects);
            var mdo = new MapDataObject()
            {
                Columns = mapData.Columns,
                Rows = mapData.Rows,
                HexTiles = mapData.HexTiles.SelectMany(x => x).ToList(),
            };
            return mdo;
        }

        private MapDataObject ProcessOverride()
        {
            var lists = mapOverride.Split('|')
                .Select(x => x.Split(";").ToList())
                .ToList();
            var mapData = new GameMapAccessor()
            {
                Columns = lists[0].Count,
                Rows = lists.Count,
                HexTiles = new(),
            };

            var rowIdx = 0;
            lists.ForEach((row) =>
            {
                var list = new List<HexTileDataObject>();
                mapData.HexTiles.Add(list);
                var colIdx = 0;
                row.ForEach((col) =>
                {
                    var hex = new HexTileDataObject()
                    {
                        ColIdx = colIdx,
                        RowIdx = rowIdx,
                        IsDiscovered = false,
                        MapAccessor = mapData,
                        TileContent = col switch
                        {
                            "W" => TileContent.Water,
                            "G" => TileContent.Empty,
                            "T" => TileContent.Tree,
                            _ => TileContent.Empty
                        },
                        ReservedForPath = false,
                    };
                    if (col == "S")
                    {
                        startTile = hex;
                    } 
                    list.Add(hex);
                    colIdx++;
                });
                rowIdx++;
            });
            var md = new MapDataObject() {
                Columns = mapData.Columns,
                Rows = mapData.Rows,
                HexTiles = mapData.HexTiles.SelectMany(x => x).ToList(),
            };
            return md;
        }

        private void InitPlayers(List<Character> players)
        {
            players.ForEach(x =>
            {
                x.Row = startTile.RowIdx;
                x.Col = startTile.ColIdx;
            });
        }

        private void PopulateMap(GameMapAccessor mapData)
        {
            GenerateIslands(mapData);

            startTile = _rnd.Next(mapData.HexTiles
                .SelectMany(x => x)
                .Where(x => x.TileContent == TileContent.Empty).ToList());
            startTile.IsDiscovered = true;
            startTile.TileContent = TileContent.City;
            var startCity = new CityEntityDataObject()
            {
                Name = "Starter town",
            };
            startTile.DetailEntity = startCity;
            startTile.ReservedForPath = true;
            startTile.GetNeighbours().ForEach(x => x.ReservedForPath = true);

            GenerateTrees(mapData);
        }

        private void GenerateIslands(GameMapAccessor mapData)
        {
            var simplex = new Simplex.Noise();
            simplex.Seed = _rnd.Next(0, 10000);
            float[,] values = simplex.Calc2D(rows, columns, waterFrequency);
            foreach (var hexTile in mapData.HexTiles.SelectMany(hexTiles => hexTiles))
            {
                if (values[hexTile.ColIdx, hexTile.RowIdx]/255 > 1 - landBias)
                {
                    hexTile.TileContent = TileContent.Empty;
                }
            }
        }

        private List<HexTileDataObject> GetAvailableLandTiles(GameMapAccessor mapData)
        {
            return mapData.HexTiles.SelectMany(hexTiles => hexTiles)
                .Where(hexTile => hexTile.TileContent == TileContent.Empty && !hexTile.ReservedForPath)
                .ToList();
        }

        private void GenerateTrees(GameMapAccessor mapData)
        {
            var treeCount = (int)(GetAvailableLandTiles(mapData).Count * treeToLandRatio);
            var treePlaced = 0;

            while (treePlaced < treeCount)
            {
                var avaliable = GetAvailableLandTiles(mapData).ShuffleInPlace(_rnd);
                var start = avaliable.FirstOrDefault();
                if (start is null)
                {
                    return;
                }

                var treeBatch = _rnd.Next(1, treeBatchMax+1);
                var current = start;
                start.ReservedForPath = true;
                for (var i = 0; i < treeBatch; ++i)
                {
                    current.TileContent = TileContentExtensions.RandomTree(_rnd);
                    var next = _rnd.Next(current.GetNeighbours().Where(x => !x.ReservedForPath && avaliable.Contains(x)).ToList());
                    current.GetNeighbours().ForEach(x => x.ReservedForPath = true);
                    if (next is null)
                    {
                        break;
                    }

                    current = next;
                    treePlaced++;
                }

                current.TileContent = TileContentExtensions.RandomTree(_rnd);
                current.GetNeighbours().ForEach(x => x.ReservedForPath = true);
            }
        }
    }
}