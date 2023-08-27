﻿using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Stackable : Item
    {
        public int Amount { get; set; } = 1;
        public StackableType StackableType { get; set; }
    }
}
