﻿using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Services.CoreNS.ItemGeneratorNS
{
    public class ItemBlueprintsUncommon
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
                CritChance = 3, 
                CritMultiplier = 100,
                Value = 10,
                Rarity = RarityType.UnCommon,
                ModelName = ItemModelName.Excalibur,
            },
            new Ranged
            {
                Name = "RANGEDWEPÖN",
                ImageName = ImageName.Bow,
                Description = "nincs nemis volt123",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Bow,
                Damage = 5,
                CritChance = 2, 
                CritMultiplier = 50,
                Value = 5,
                Rarity = RarityType.UnCommon,
                ModelName = ItemModelName.Excalibur,
            },
            new Melee
            {
                Name = "PointyWEPÖN",
                ImageName = ImageName.Spear,
                Description = "nincs nemis volt",
                ItemType = ItemType.Weapon,
                WeaponType = WeaponType.Spear,
                Damage = 8,
                CritChance = 3, 
                CritMultiplier = 75,
                Value = 15,
                Rarity = RarityType.UnCommon,
                ModelName = ItemModelName.Excalibur,
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
                Rarity = RarityType.UnCommon
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
                Rarity = RarityType.UnCommon
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
                Rarity = RarityType.UnCommon
            },
            new Hand{
                Name = "HoboGloves",
                ImageName = ImageName.Hand,
                Description = "in case of emergency",
                ArmorType = ArmorType.Hand,
                ItemType = ItemType.Armor,
                ArmorValue = 1,
                Health = 1.1,
                Value = 15,
                Rarity = RarityType.UnCommon
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
                Rarity = RarityType.UnCommon
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
                Rarity = RarityType.UnCommon
            },
            new Material{
                Name = "Stón",
                ImageName = ImageName.Stone,
                Description = "not quite The Rock",
                ItemType = ItemType.Material,
                StackableType = StackableType.Stone2,
                Value = 1,
                Rarity = RarityType.UnCommon
            },
        };
    }
}
