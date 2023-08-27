﻿using InfiniteCreativity.Models;
using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Materials;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public class ItemBlueprintsRare
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
                Damage = 10,
                AttackSpeed = 3,
                CritChance = 3,
                CritMultiplier = 100,
                Range = 1,
                Value = 10,
                Rarity = RarityType.Rare
            },
            new Ranged
            {
                Name = "RANGEDWEPÖN",
                ImageName = ImageName.Bow,
                Description = "nincs nemis volt123",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Bow,
                Damage = 5,
                AttackSpeed = 2,
                CritChance = 2,
                CritMultiplier = 50,
                Range = 10,
                Value = 5,
                Rarity = RarityType.Rare
            },
            new Melee
            {
                Name = "PointyWEPÖN",
                ImageName = ImageName.Spear,
                Description = "nincs nemis volt",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Spear,
                Damage = 8,
                AttackSpeed = 2,
                CritChance = 3,
                CritMultiplier = 75,
                Range = 5,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Head{
                Name = "Bucket",
                ImageName = ImageName.Head,
                Description = "without water",
                ArmorType = ArmorType.Head,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Shoulder{
                Name = "Stripe",
                ImageName = ImageName.Shoulder,
                Description = "you did not earn it",
                ArmorType = ArmorType.Shoulder,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Chest{
                Name = "Apron",
                ImageName = ImageName.Chest,
                Description = "bruh",
                ArmorType = ArmorType.Chest,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Hand{
                Name = "HoboGloves",
                ImageName = ImageName.Hand,
                Description = "in case of emergency",
                ArmorType = ArmorType.Hand,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Leg{
                Name = "Shorts",
                ImageName = ImageName.Leg,
                Description = "out of ideas",
                ArmorType = ArmorType.Leg,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Boot{
                Name = "FlipFlop",
                ImageName = ImageName.Boot,
                Description = "secound part indicates what will happen",
                ArmorType = ArmorType.Boot,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Movement = 1,
                Value = 15,
                Rarity = RarityType.Rare
            },
            new Material{
                Name = "Stón",
                ImageName = ImageName.Stone,
                Description = "getting closer to The Rock",
                ItemType = ItemType.Material,
                StackableType = StackableType.Stone3,
                Value = 1,
                Rarity = RarityType.Rare
            },
        };
    }
}