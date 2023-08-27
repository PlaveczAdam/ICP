using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Services
{
    public static class EffectiveStatComputer
    {
        internal static double ComputeEffectiveAbilityDamage(double baseAbilityDamage)
        {
            return baseAbilityDamage;
        }

        internal static double ComputeEffectiveAbilityResource(double baseAbilityResource)
        {
            return baseAbilityResource;
        }

        internal static double ComputeEffectiveAbilityResourceGain(double baseAbilityResourceGain)
        {
            return baseAbilityResourceGain;
        }

        internal static double ComputeEffectiveDamage(double baseDamage, Weapon? weapon)
        {
            if(weapon == null) { 
                return baseDamage;
            }
            return baseDamage * weapon.Damage;
        }

        internal static double ComputeEffectiveDefense(List<Armor> armor)
        {
            return armor.Aggregate(0.0, (prev, curr) => prev + curr.ArmorValue);
        }

        internal static double ComputeEffectiveHealth(double baseHealth, List<Armor> armor)
        {
            return baseHealth * armor.Aggregate(1.0, (prev, curr)=> prev * curr.Health);
        }

        internal static double ComputeEffectiveMovement(int baseMovement, Boot? boot)
        {
            if(boot == null) {
                return baseMovement; 
            }
            return baseMovement + boot.Movement;
        }
    }
}
