﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Character
    {
        private Random _rnd = new Random();
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Level { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public int CurrentMovement { get; set; }
        public double CurrentAbilityResource { get; set; }
        public BattleParticipant? BattleParticipant { get; set; }

        private double _currentHealth;
        public double CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (_currentHealth > 0 && value <= 0)
                {
                    Die();
                }
                _currentHealth = value;
            }
        }

        private void Die()
        {
            BattleParticipant.Buffs.Clear();
            BattleParticipant.Conditions.Clear();
            BattleParticipant.OwnedMinions.ForEach(x => x.OwnerDeath());

        }


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
        public double BaseCriticalChance => StatComputer.ComputeBaseCriticalChance(Profession);
        [NotMapped]
        public double BaseCriticalMultiplier => StatComputer.ComputeBaseCriticalMultiplier(Profession);
        [NotMapped]
        public double BaseSpeed => StatComputer.ComputeBaseSpeed(Profession);

        [NotMapped]
        public double Health => EffectiveStatComputer.ComputeEffectiveHealth(BaseHealth, Armor);
        [NotMapped]
        public int Movement => EffectiveStatComputer.ComputeEffectiveMovement(BaseMovement, Boot);
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
        [NotMapped]
        public double CriticalChance => EffectiveStatComputer.ComputeEffectiveCriticalChance(BaseCriticalChance, Weapon);
        [NotMapped]
        public double CriticalMultiplier => EffectiveStatComputer.ComputeEffectiveCritMultiplier(BaseCriticalMultiplier, Weapon);
        [NotMapped]
        public double Speed => EffectiveStatComputer.ComputeEffectiveSpeed(BaseSpeed);

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        [NotMapped]
        public List<Armor> Armor => new List<Armor?>() { Head, Shoulder, Chest, Hand, Leg, Boot }.Where(x => x is not null).ToList<Armor>();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        public IEnumerable<Quest>? Quests { get; set; }

        public bool IsInCombat { get; set; }
        public Head? Head { get; set; }
        public Shoulder? Shoulder { get; set; }
        public Chest? Chest { get; set; }
        public Hand? Hand { get; set; }
        public Leg? Leg { get; set; }
        public Boot? Boot { get; set; }
        public Weapon? Weapon { get; set; }

        public Player Player { get; set; }
        public ICollection<CharacterSkillSlot> SkillSlots { get; set; }

        public int? Row { get; set; }
        public int? Col { get; set; }

        
        public void TakeHealing(double heal)
        {
            if (CurrentHealth < Health)
            { 
                CurrentHealth += heal;
                if (CurrentHealth > Health)
                {
                    CurrentHealth = Health;
                }
            }
        }
        public void TakeDamage(double damage, StatModifications modifications)
        {
            CurrentHealth -= Math.Max(damage - (Defense * modifications.DefenseMultiplier), 0);
        }

        public void TakeConditionDamage(double cDamage)
        {
            CurrentHealth -= Math.Max(cDamage, 0);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            }
        }

        public IEnumerable<ShowBattleEventDTO> AutoAttack(BattleParticipant enemy, BattleParticipant attacker, IMapper mapper)
        {
            var crit = _rnd.NextCrit(CriticalChance);
            var modifiers = attacker.CalculateStatModifications();
            var damage = Damage * Math.Pow(CriticalMultiplier, crit) * modifiers.DamageMultiplier;

            enemy.TakeDamage(damage, enemy.CalculateStatModifications());
            attacker.CurrentActionGauge -= 1;

            var res = new List<ShowBattleEventDTO>(){
                new ShowBattleEventAutoAttackDTO(){
                    SourceParticipantId = attacker.Id,
                    TargetParticipantId = enemy.Id,
                    NewTargetHp = enemy.CurrentHealth,
                    NewAbilityGauge = attacker.CurrentActionGauge,
                }
            };

            if (!enemy.IsAlive)
            {
                res.Add(new ShowBattleEventParticipantDiesDTO()
                {
                    SourceParticipantId = attacker.Id,
                    TargetParticipantId = enemy.Id,
                    MinionsChangingSide = mapper.Map<List<ShowBattleParticipantDTO>>(
                        enemy.OwnedMinions
                            .Where(x => x.Side != enemy.Side)
                            .Select(x => x.BattleParticipant)),
                });
            }

            return res;
        }

        
    }
}
