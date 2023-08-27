using System;
using System.Collections.Generic;
using DataObjects;
using Entities;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.Enums.GameNS;

namespace Extensions
{
    public static class TileContentExtensions
    {
        private static readonly List<TileContent> treeList = new()
        {
            TileContent.Tree, TileContent.Tree2
        };

        public static EntityBaseDataObject CreateEmptyEntity(this TileContent type)
        {
            return type switch
            {
                TileContent.City => new CityEntityDataObject() { Name = "", },
                _ => throw new NotImplementedException()
            };
        }

        public static TileContent RandomTree(Random rnd)
        {
            return rnd.Next(treeList);
        }

        public static bool IsWalkable(this TileContent type)
        {
            return type is TileContent.Empty or TileContent.City or TileContent.Dungeon or TileContent.Water;
        }
    }
}