using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Services.ItemGeneratorNS
{
    public class ItemGenerator
    {
        private Random _random = new Random();

        public Item Generate()
        {
            var itemDesc = _random.Next(ItemBlueprints.ItemDescriptions).ShallowCopy();
            switch (itemDesc)
            {
                case Weapon weapon:
                    return GenerateWeapon(weapon);
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
                /* case Ranged ranged:
                     return GenerateRanged(ranged);*/
                default:
                    throw new NotImplementedException();
            }
        }

        private Melee GenerateMelee(Melee itemDesc)
        {
            return itemDesc;
        }
    }
}
