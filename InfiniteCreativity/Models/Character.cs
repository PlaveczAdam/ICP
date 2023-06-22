﻿using System.Collections;

namespace InfiniteCreativity.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Purse { get; set; }
        public IEnumerable<Item>? Inventory { get; set; }
        public IEnumerable<Quest>? Quests { get; set; }
    }
}
