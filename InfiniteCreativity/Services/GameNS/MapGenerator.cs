using DTOs.Game;
using Entities;
using Extensions;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Services.GameNS;
using InfiniteCreativity.Services.MapPatherNS;
using MoreLinq.Extensions;
using System.IO;

namespace Map
{
    public class MapGenerator
    {
        private string mapOverride = "";
        private HexTileDataObject startTile;
        private Random _rnd = new Random();

        public MapDataObject GenerateAndPlacePlayer(List<Character> playersDataObjects, MapGeneratorSettings settings)
        {
            if (mapOverride.Length > 0)
            {
                var md = ProcessOverride();
                InitPlayers(playersDataObjects);
                return md;
            }
            var mapData = new GameMapAccessor()
            {
                Columns = settings.Columns,
                Rows = settings.Rows,
                HexTiles = new(),
            };

            for (var row = 0; row < settings.Rows; row++)
            {
                var hexTiles = new List<HexTileDataObject>();
                for (var column = 0; column < settings.Columns; column++)
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

            PopulateMap(mapData, settings);
            GenerateRoads(mapData);
            InitPlayers(playersDataObjects);
            var mdo = new MapDataObject()
            {
                Columns = mapData.Columns,
                Rows = mapData.Rows,
                HexTiles = mapData.HexTiles.SelectMany(x => x).ToList(),
            };
            return mdo;
        }

        private void GenerateRoads(GameMapAccessor mapData)
        {
            var cityWithState = new Dictionary<HexTileDataObject, int>();
            mapData.HexTiles.SelectMany(x => x).Where(x => x.TileContent == TileContent.City).ForEach(x => cityWithState.Add(x, 0));
            HexTileDataObject? previousCity = null;
            HexTileDataObject? nextCity = null;

            while (cityWithState.Any(x => x.Value < 2))
            { 
                var currentCity = nextCity ?? cityWithState.First(x => x.Value < 2).Key;
                (List<HexTileDataObject> path, HexTileDataObject? connectedToCity) = FindNearest(currentCity, x => x.TileContent == TileContent.City && x != previousCity && x != currentCity && (previousCity is null || cityWithState[x] == 0));
                cityWithState[currentCity] = 2;
                nextCity = (connectedToCity != null && cityWithState[connectedToCity] == 2) ? null : connectedToCity;
                path.ForEach(x => {
                    if (x.TileContent == TileContent.Empty)
                    {
                        x.TileContent = TileContent.Road;
                    }
                });
                previousCity = connectedToCity is null ? null : currentCity;
            }
        }

        private List<HexTileDataObject> GeneratePath(GraphNode currentNode)
        {
            var path = new List<HexTileDataObject>();

            var curr = currentNode;
            while (curr != null)
            {
                path.Add(curr.Data);
                curr = curr.Parent;
            }
            path.Reverse();
            path.RemoveAt(0);
            return path;

        }

        private (List<HexTileDataObject> path, HexTileDataObject? connectedToCity) FindNearest(HexTileDataObject currentCity, Func<HexTileDataObject, bool> condition)
        {
            var startNode = new GraphNode(currentCity)
            {
                TotalCost = 0
            };
            var openNodes = new PriorityQueue<GraphNode>();
            openNodes.Enqueue(startNode, 0);
            var closedNodes = new HashSet<GraphNode>();
            while (!openNodes.IsEmpty())
            {
                var currentNode = openNodes.Dequeue();
                if (condition(currentNode.Data))
                {
                    return (GeneratePath(currentNode), currentNode.Data);
                }
                closedNodes.Add(currentNode);
                var neighbours = currentNode.Data.GetNeighbours().Where(x => x.TileContent != TileContent.Water).ToList();
                foreach (var neighbour in neighbours)
                {
                    var neighbourNode = openNodes.FirstOrDefault(x => x.Data == neighbour) ??
                                        new GraphNode(neighbour);
                    if (!neighbour.IsWalkable())
                    {
                        closedNodes.Add(neighbourNode);
                    }

                    if (closedNodes.Contains(neighbourNode))
                    {
                        continue;
                    }

                    var newCost = currentNode.TotalCost + 1;

                    if (openNodes.Contains(neighbourNode))
                    {
                        if (newCost >= neighbourNode.TotalCost)
                        {
                            continue;
                        }

                        openNodes.Remove(neighbourNode);
                    }

                    neighbourNode.TotalCost = newCost;
                    neighbourNode.Parent = currentNode;
                    openNodes.Enqueue(neighbourNode, neighbourNode.TotalCost);
                }
            }
            return (new (), null);
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

        private void PopulateMap(GameMapAccessor mapData, MapGeneratorSettings settings)
        {
            GenerateIslands(mapData, settings);

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

            PlaceFragments(mapData, settings);
            GenerateTrees(mapData, settings);
        }

        private void PlaceFragments(GameMapAccessor mapData, MapGeneratorSettings settings)
        {
            var fragments = settings.MapFragmentPresets;
            fragments.ShuffleInPlace(_rnd);
            CalculateFreeNeighbours(mapData);
            foreach (var currentFragment in fragments)
            {
                var candidates = mapData.HexTiles.SelectMany(x => x).Where(x => x.RowIdx % 2 != 0 && x.CanFragmentFit(currentFragment)).Shuffle();
                var actualPosition = candidates.FirstOrDefault(x => DoesPresetFragmentFit(mapData, x, currentFragment));
                if (actualPosition is null)
                {
                    continue;
                }
                PlaceFragment(mapData, actualPosition, currentFragment);
                CalculateFreeNeighbours(mapData);
            }
        }

        private void PlaceFragment(GameMapAccessor mapData, HexTileDataObject actualPosition, MapFragmentPresetDTO currentFragment)
        {
            for (int i = 0; i < currentFragment.Map.Count; i++)
            {
                for (int j = 0; j < currentFragment.Map[i].Count; j++)
                {
                    var maptile = mapData.HexTiles[i + actualPosition.RowIdx][j + actualPosition.ColIdx];
                    var fragmentTile = currentFragment.Map[i][j];
                    maptile.ReservedForPath = true;
                    maptile.TileContent = fragmentTile.Content;
                    if (fragmentTile.Enemy is not null)
                    {
                        maptile.EnemyBlueprint = new EnemyBlueprint()
                        {
                            Type = fragmentTile.Enemy.Type,
                            IsBoss = fragmentTile.Enemy.IsBoss,
                        };
                    }
                }
            }
        }

        private void GenerateIslands(GameMapAccessor mapData, MapGeneratorSettings settings)
        {
            var simplex = new Simplex.Noise();
            simplex.Seed = _rnd.Next(0, 10000);
            float[,] values = simplex.Calc2D(settings.Rows, settings.Columns, settings.WaterFrequency);
            foreach (var hexTile in mapData.HexTiles.SelectMany(hexTiles => hexTiles))
            {
                if (values[hexTile.ColIdx, hexTile.RowIdx]/255 > 1 - settings.LandBias)
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
        private void CalculateFreeNeighbours(GameMapAccessor mapData)
        {
            for (int i = 0; i < mapData.Rows; i++)
            {
                var leftNeighbours = new List<HexTileDataObject>();
                for (int j = 0; j < mapData.Columns; j++)
                {
                    var current = mapData.HexTiles[i][j];
                    current.FreeNeighboursRight = current.ReservedForPath?0:1;

                    if (current.ReservedForPath || j == mapData.Columns - 1)
                    {
                        for(int k = 0; k < leftNeighbours.Count; k++)
                        {
                            leftNeighbours[k].FreeNeighboursRight = leftNeighbours.Count - k;
                        }
                        leftNeighbours.Clear();
                    }
                    else
                    { 
                        leftNeighbours.Add(current);
                    }
                }
            }

            for (int i = 0; i < mapData.Columns; i++)
            {
                var upNeighbours = new List<HexTileDataObject>();
                for (int j = 0; j < mapData.Rows; j++)
                {
                    var current = mapData.HexTiles[j][i];
                    current.FreeNeighboursDown = current.ReservedForPath ? 0 : 1;

                    if (current.ReservedForPath || j == mapData.Rows - 1)
                    {
                        for (int k = 0; k < upNeighbours.Count; k++)
                        {
                            upNeighbours[k].FreeNeighboursDown = upNeighbours.Count - k;
                        }
                        upNeighbours.Clear();
                    }
                    else
                    {
                        upNeighbours.Add(current);
                    }
                }
            }
        }

        public bool DoesPresetFragmentFit(GameMapAccessor map, HexTileDataObject topLeftCandidate, MapFragmentPresetDTO fragment)
        {
            for (int i = 1; i < fragment.Map.Count; i++)
            { 
                var tile = map.GetTileByIndex(topLeftCandidate.RowIdx+i, topLeftCandidate.ColIdx);
                if (tile.FreeNeighboursRight < fragment.Map[i].Count)
                {
                    return false;
                }
            }
            return true;
        }

        private void GenerateTrees(GameMapAccessor mapData, MapGeneratorSettings settings)
        {
            var treeCount = (int)(GetAvailableLandTiles(mapData).Count * settings.TreeToLandRatio);
            var treePlaced = 0;

            while (treePlaced < treeCount)
            {
                var avaliable = GetAvailableLandTiles(mapData).ShuffleInPlace(_rnd);
                var start = avaliable.FirstOrDefault();
                if (start is null)
                {
                    return;
                }

                var treeBatch = _rnd.Next(1, settings.TreeBatchMax +1);
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