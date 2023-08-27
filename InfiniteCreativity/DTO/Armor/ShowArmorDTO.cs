﻿using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO.Armor
{
    public class ShowArmorDTO : ShowEquippableDTO
    {
        public ArmorType ArmorType { get; set; }
        public double ArmorValue { get; set; }
    }

}
