using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Models;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public static class ItemBlueprints
    {
        public static List<Item> ItemDescriptions = new List<Item>()
        {
            new Melee
            {
                Name = "WEPÖN",
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
            /*new Ranged
            {
                Name = "WEPÖN",
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
            },*/
            new Melee
            {
                Name = "WEPÖN",
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
        };
    }
}
