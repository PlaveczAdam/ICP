﻿using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Models.CoreNS.Weapons
{
    public class Weapon : Equippable
    {
        public WeaponType WeaponType { get; set; }
        public double Damage { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
    }
}
