using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Services.CoreNS.ItemGeneratorNS
{
    public static class ItemBlueprintsCommon
    {
        public static List<Item> ItemDescriptions = new List<Item>()
        {
            /*new Melee
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
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
                Rarity = RarityType.Common
            },
            new Material{
                Name = "Stóne",
                ImageName = ImageName.Stone,
                Description = "defenatly not The Rock",
                ItemType = ItemType.Material,
                StackableType = StackableType.Stone1,
                Value = 1,
                Rarity = RarityType.Common
            },*/
            new Material{
                Name = "Fizsh",
                ImageName = ImageName.Stone,
                Description = "slap n eat",
                ItemType = ItemType.Material,
                StackableType = StackableType.Fish,
                Value = 1,
                Rarity = RarityType.Common
            },
            Skill.SkillHolder[StackableType.FirstSkill],
            Skill.SkillHolder[StackableType.HealSkill],
            Skill.SkillHolder[StackableType.ContinousBuff],
            Skill.SkillHolder[StackableType.GenericDebuff],
            Skill.SkillHolder[StackableType.BigBleed],
            Skill.SkillHolder[StackableType.BigProtection],
            Skill.SkillHolder[StackableType.Taunt],
            Skill.SkillHolder[StackableType.BB],
        };
    }
}
