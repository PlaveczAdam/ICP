using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Materials;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public class ItemGenerator
    {
        private Random _random = new Random();
        private int _superRareTreshold = 990;
        private int _rareTreshold = 900;
        private int _uncommonTreshold = 600;

        public Item Generate()
        {
            Item item;
            int rarity = _random.Next(1, 1000);
            if (rarity >= _superRareTreshold)
            {
                item = _random.Next(ItemBlueprintsSuperRare.ItemDescriptions).ShallowCopy();
            }
            else if (rarity >= _rareTreshold)
            {
                item = _random.Next(ItemBlueprintsRare.ItemDescriptions).ShallowCopy();
            }
            else if (rarity >= _uncommonTreshold)
            {
                item = _random.Next(ItemBlueprintsUncommon.ItemDescriptions).ShallowCopy();
            }
            else
            {
                item = _random.Next(ItemBlueprintsCommon.ItemDescriptions).ShallowCopy();
            }

            switch (item)
            {
                case Weapon weapon:
                    return GenerateWeapon(weapon);
                case Armor armor:
                    return GenerateArmor(armor);
                case Material material:
                    return GenerateMaterial(material);
                default:
                    throw new NotImplementedException();
            }
        }

        private Weapon GenerateWeapon(Weapon itemDesc)
        {
            itemDesc.Damage *= _random.NextDouble(0.9, 1.1);
            itemDesc.CritChance *= _random.NextDouble(0.9, 1.1);
            itemDesc.CritMultiplier *= _random.NextDouble(0.9, 1.1);
            switch (itemDesc)
            {
                case Melee melee:
                    return GenerateMelee(melee);
                case Ranged ranged:
                    return GenerateRanged(ranged);
                default:
                    throw new NotImplementedException();
            }
        }

        private Armor GenerateArmor(Armor itemDesc)
        {
            itemDesc.ArmorValue *= _random.NextDouble(0.9, 1.1);
            itemDesc.Health *= _random.NextDouble(0.9, 1.1);
            return itemDesc;
        }

        private Melee GenerateMelee(Melee itemDesc)
        {
            return itemDesc;
        }

        private Ranged GenerateRanged(Ranged itemDesc)
        {
            itemDesc.Reload *= _random.NextDouble(0.9, 1.1);
            return itemDesc;
        }
        private Material GenerateMaterial(Material itemDesc)
        {
            return itemDesc;
        }
    }
}
