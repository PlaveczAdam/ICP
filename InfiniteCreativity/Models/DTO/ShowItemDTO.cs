﻿using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Models.DTO
{
    public abstract class ShowItemDTO
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RarityType Rarity { get; set; }
        public int? Value { get; set; }
        public ItemType ItemType { get; init; }
    }
}
