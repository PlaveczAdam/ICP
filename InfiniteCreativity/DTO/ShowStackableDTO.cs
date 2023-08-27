﻿using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO
{
    public class ShowStackableDTO : ShowItemDTO
    {
        public int Amount { get; set; }
        public int StackSize { get; set; }
        public StackableType StackableType { get; set; }
    }
}
