using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Services;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Level { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }

        [NotMapped]
        public double BaseHealth => StatComputer.ComputeBaseHealth(Race, Profession, Level);
        [NotMapped]
        public int BaseMovement => StatComputer.ComputeBaseMovement(Race, Profession);
        [NotMapped]
        public double BaseDamage => StatComputer.ComputeBaseDamage(Race, Profession, Level);
        [NotMapped]
        public double BaseAbilityDamage => StatComputer.ComputeBaseAbilityDamage(Race, Profession, Level);
        [NotMapped]
        public double BaseAbilityResource => StatComputer.ComputeBaseAbilityResource(Race, Profession);
        [NotMapped]
        public double BaseAbilityResourceGain => StatComputer.ComputeBaseAbilityResourceGain(Race, Profession);

        [NotMapped]
        public double Health => EffectiveStatComputer.ComputeEffectiveHealth(BaseHealth, Armor);
        [NotMapped]
        public double Movement => EffectiveStatComputer.ComputeEffectiveMovement(BaseMovement, Boot);
        [NotMapped]
        public double Damage => EffectiveStatComputer.ComputeEffectiveDamage(BaseDamage, Weapon);
        [NotMapped]
        public double AbilityDamage => EffectiveStatComputer.ComputeEffectiveAbilityDamage(BaseAbilityDamage);
        [NotMapped]
        public double AbilityResource => EffectiveStatComputer.ComputeEffectiveAbilityResource(BaseAbilityResource);
        [NotMapped]
        public double AbilityResourceGain => EffectiveStatComputer.ComputeEffectiveAbilityResourceGain(BaseAbilityResourceGain);
        [NotMapped]
        public double Defense => EffectiveStatComputer.ComputeEffectiveDefense(Armor);

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        [NotMapped]
        public List<Armor> Armor => (new List<Armor?>() { Head, Shoulder, Chest, Hand, Leg, Boot}).Where(x=>x is not null).ToList<Armor>();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        public IEnumerable<Quest>? Quests { get; set; }

        public Head? Head { get; set; }
        public Shoulder? Shoulder { get; set; }
        public Chest? Chest { get; set; }
        public Hand? Hand { get; set; }
        public Leg? Leg { get; set; }
        public Boot? Boot { get; set; }
        public Weapon? Weapon { get; set; }

        public Player Player { get; set; }
    }
}
