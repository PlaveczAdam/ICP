using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Enemy
    {
        private Random _rnd = new Random();

        public Guid Id { get; set; }
        public double Level { get; set; }
        public EnemyType EnemyType { get; set; }

        public double Health { get; set; }
        public GConnection GConnection { get; set; }
        public HexTileDataObject? Tile { get; set; }

        [NotMapped]
        public string Name => EnemyType.ToString();
        [NotMapped]
        public virtual double MaxHealth => EnemyStatComputer.CalculateMaxHealth(Level, EnemyType);
        [NotMapped]
        public double Defense => EnemyStatComputer.CalculateDefense(Level, EnemyType);
        [NotMapped]
        public double Damage => EnemyStatComputer.CalculateDamage(Level, EnemyType);
        [NotMapped]
        public double CriticalChance => EnemyStatComputer.CalculateCriticalChance(Level, EnemyType);
        [NotMapped]
        public double CriticalMultiplier => EnemyStatComputer.CalculateCriticalMultiplier(Level, EnemyType);
        [NotMapped]
        public double Speed => EnemyStatComputer.CalculateSpeed(EnemyType);

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> characterParticipants, BattleParticipant selfParticipant)
        {
            var result = new List<ShowBattleEventDTO>();
            var modifiers = selfParticipant.CalculateStatModifications();

            while (selfParticipant.CurrentActionGauge > 0)
            {
                var validTargets = characterParticipants
                    .Where(x => x.Character.CurrentHealth > 0);
                if (validTargets.Count() == 0)
                {
                    return result;
                }
                var target = validTargets
                    .MaxBy(x => x.Character!.Defense);
                var targetModifiers = target.CalculateStatModifications();
                target!.Character!.TakeDamage(CalculateDamage(modifiers), targetModifiers);

                result.Add(new ShowBattleEventEnemyAttackDTO()
                {
                    SourceParticipantId = selfParticipant.Id,
                    TargetParticipantId = target.Id,
                    NewTargetHp = target.Character.CurrentHealth
                });
                if (target.Character.CurrentHealth <= 0)
                {
                    result.Add(new ShowBattleEventParticipantDiesDTO()
                    {
                        SourceParticipantId = selfParticipant.Id,
                        TargetParticipantId = target.Id,
                    });
                    target.Buffs.Clear();
                    target.Conditions.Clear();
                }

                selfParticipant.CurrentActionGauge--;
            }

            return result;
        }
        public double CalculateDamage(StatModifications modifications)
        {
            var critPower = _rnd.NextCrit(CriticalChance);
            var critMultiplier = Math.Pow(CriticalMultiplier, critPower);

            return Damage * critMultiplier * modifications.DamageMultiplier;
        }

        public void TakeDamage(double damage, StatModifications modifications)
        {
            Health -= Math.Max(damage - (Defense * modifications.DefenseMultiplier), 0);
        }

        public void TakeConditionDamage(double cDamage)
        {
            Health -= Math.Max(cDamage, 0);

            if (Health <= 0)
            {
                Health = 0;
            }
        }
    }
}
