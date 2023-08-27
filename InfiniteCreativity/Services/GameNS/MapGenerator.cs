using System.Collections.Generic;
using System.Linq;
using DataObjects;
using Entities;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] public int rows = 10;
        [SerializeField] public int columns = 10;
        [SerializeField, Range(0, 1f)] private float landBias = 0.45f;
        [SerializeField] private float waterFrequency = 2f;
        [SerializeField] [Range(0, 1f)] private float treeToLandRatio = 0.25f;
        [SerializeField] private int treeBatchMax = 10;
        [SerializeField] private string mapOverride = "";
        private HexTileDataObject startTile;

        public MapDataObject GenerateAndPlacePlayer(List<CharacterDataObject> playersDataObjects)
        {
            if (mapOverride.Length > 0)
            {
                var md = ProcessOverride();
                InitPlayers(playersDataObjects);
                return md;
            }
            var mapData = new MapDataObject()
            {
                Columns = columns,
                Rows = rows,
                HexTiles = new List<List<HexTileDataObject>>(),
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
                        Map = mapData
                    };
                    hexTiles.Add(hexTile);
                }

                mapData.HexTiles.Add(hexTiles);
            }

            PopulateMap(mapData);
            InitPlayers(playersDataObjects);

            return mapData;
        }

        private MapDataObject ProcessOverride()
        {
            var lists = mapOverride.Split('|')
                .Select(x => x.Split(";").ToList())
                .ToList();
            var mapData = new MapDataObject()
            {
                Columns = lists[0].Count,
                Rows = lists.Count,
                HexTiles = new List<List<HexTileDataObject>>(),
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
                        Map = mapData,
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
            return mapData;
        }

        private void InitPlayers(List<CharacterDataObject> players)
        {
            players.ForEach(x =>
            {
                x.Row = startTile.RowIdx;
                x.Col = startTile.ColIdx;
            });
        }

        private void PopulateMap(MapDataObject mapData)
        {
            GenerateIslands(mapData);

            startTile = mapData.HexTiles
                .SelectMany(x => x)
                .Where(x => x.TileContent == TileContent.Empty).ToList()
                .GetRandom();
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

        private void GenerateIslands(MapDataObject mapData)
        {
            var offsetX = Random.value * 10000f;
            var offsetY = Random.value * 10000f;
            foreach (var hexTile in mapData.HexTiles.SelectMany(hexTiles => hexTiles))
            {
                if (Mathf.PerlinNoise(offsetX + hexTile.ColIdx / (float)columns * waterFrequency,
                        offsetY + hexTile.RowIdx / (float)rows * waterFrequency) > 1 - landBias)
                {
                    hexTile.TileContent = TileContent.Empty;
                }
            }
        }

        private List<HexTileDataObject> GetAvailableLandTiles(MapDataObject mapData)
        {
            return mapData.HexTiles.SelectMany(hexTiles => hexTiles)
                .Where(hexTile => hexTile.TileContent == TileContent.Empty && !hexTile.ReservedForPath)
                .ToList();
        }

        private void GenerateTrees(MapDataObject mapData)
        {
            var treeCount = (int)(GetAvailableLandTiles(mapData).Count * treeToLandRatio);
            var treePlaced = 0;

            while (treePlaced < treeCount)
            {
                var avaliable = GetAvailableLandTiles(mapData).ShuffleInPlace();
                var start = avaliable.FirstOrDefault();
                if (start is null)
                {
                    this.LogWarning("no more land tiles");
                    return;
                }

                var treeBatch = RandomExtensions.NextIntInclusive(1, treeBatchMax);
                var current = start;
                start.ReservedForPath = true;
                for (var i = 0; i < treeBatch; ++i)
                {
                    current.TileContent = TileContentExtensions.RandomTree();
                    var next = current.GetNeighbours().Where(x => !x.ReservedForPath && avaliable.Contains(x)).ToList()
                        .GetRandom();
                    current.GetNeighbours().ForEach(x => x.ReservedForPath = true);
                    if (next is null)
                    {
                        break;
                    }

                    current = next;
                    treePlaced++;
                }

                current.TileContent = TileContentExtensions.RandomTree();
                current.GetNeighbours().ForEach(x => x.ReservedForPath = true);
            }
        }
    }
}