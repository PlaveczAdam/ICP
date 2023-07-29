﻿using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Armor;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public static class ItemBlueprints
    {
        public static List<Item> ItemDescriptions = new List<Item>()
        {
            new Melee
            {
                Name = "WEPÖN",
                ImageName = ImageName.Sword,
                Description = "nincs nemis volt",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Sword,
                StackSize = 1,
                Damage = 10,
                AttackSpeed = 3,
                CritChance = 3,
                CritMultiplier = 100,
                Range = 1,
                Value = 10
            },
            new Ranged
            {
                Name = "RANGEDWEPÖN",
                ImageName = ImageName.Bow,
                Description = "nincs nemis volt123",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Bow,
                StackSize = 1,
                Damage = 5,
                AttackSpeed = 2,
                CritChance = 2,
                CritMultiplier = 50,
                Range = 10,
                Value = 5
            },
            new Melee
            {
                Name = "PointyWEPÖN",
                ImageName = ImageName.Spear,
                Description = "nincs nemis volt",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Spear,
                StackSize = 1,
                Damage = 8,
                AttackSpeed = 2,
                CritChance = 3,
                CritMultiplier = 75,
                Range = 5,
                Value = 15
            },
            new Head{ 
                Name = "Bucket",
                ImageName = ImageName.Armor,
                Description = "without water",
                ArmorType = ArmorType.Head,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
            new Shoulder{
                Name = "Stripe",
                ImageName = ImageName.Armor,
                Description = "you did not earn it",
                ArmorType = ArmorType.Shoulder,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
            new Chest{
                Name = "Apron",
                ImageName = ImageName.Armor,
                Description = "bruh",
                ArmorType = ArmorType.Chest,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
            new Hand{
                Name = "HoboGloves",
                ImageName = ImageName.Armor,
                Description = "in case of emergency",
                ArmorType = ArmorType.Hand,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
            new Leg{
                Name = "Shorts",
                ImageName = ImageName.Armor,
                Description = "out of ideas",
                ArmorType = ArmorType.Leg,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
            new Boot{
                Name = "FlipFlop",
                ImageName = ImageName.Armor,
                Description = "secound part indicates what will happen",
                ArmorType = ArmorType.Boot,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                StackSize = 1,
                Value = 15,
            },
        };
    }
}
