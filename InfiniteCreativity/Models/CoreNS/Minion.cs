using AutoMapper;
using DTOs.Enums.GameNS;
using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Models.GameNS.Enemys;
using MoreLinq;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public abstract class Minion
    {
        protected Random _rnd = new Random();
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public BattleParticipant Caster { get; set; }
        public Guid CasterId { get; set; }
        public int? CurrentDuration { get; set; }
        public BattleParticipant BattleParticipant { get; set; }
        public Side Side { get; set; }
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
        }


        [NotMapped]
        public abstract string Name { get; }
        [NotMapped]
        public abstract double MaxHealth { get; }
        [NotMapped]
        public abstract MinionType Type { get; }
        [NotMapped]
        public abstract int? Duration { get; }
        [NotMapped]
        public abstract double Defense { get; }
        [NotMapped]
        public abstract double CriticalChance { get; }
        [NotMapped]
        public abstract double CriticalMultiplier { get; }
        [NotMapped]
        public abstract double Damage { get; }
        [NotMapped]
        protected virtual Side SideAfterDeath => Caster.Side;
        [NotMapped]
        public abstract double Speed { get;}

        protected abstract BattleParticipant? SelectNonForceTarget(List<BattleParticipant> aliveFriendly, List<BattleParticipant> aliveEnemy);
        protected abstract List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifiers, StatModifications selfModifiers);

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> allParticipant)
        {
            var result = new List<ShowBattleEventDTO>();
            var forceTarget = BattleParticipant.Conditions.FirstOrDefault(x => x is Taunt)?.Caster;

            while (BattleParticipant.CurrentActionGauge > 0)
            {
                var aliveParticipants = allParticipant.Where(x => x.IsAlive).ToList();
                var aliveFriendly = aliveParticipants.Where(x => x.Side == BattleParticipant.Side).ToList();
                var aliveEnemy = aliveParticipants.Where(x => x.Side != BattleParticipant.Side).ToList();
                var selfModifiers = BattleParticipant.CalculateStatModifications();

                if (!forceTarget?.IsAlive??false)
                {
                    forceTarget = null;
                }

                var target = forceTarget ?? SelectNonForceTarget(aliveFriendly, aliveEnemy);

                if (target is null)
                {
                    return result;
                }

                var targetModifiers = target.CalculateStatModifications();

                result.AddRange(ActionTurn(target, targetModifiers, selfModifiers));

                if (!target.IsAlive)
                {
                    result.Add(new ShowBattleEventParticipantDiesDTO()
                    {
                        SourceParticipantId = BattleParticipant.Id,
                        TargetParticipantId = target.Id,
                    });
                }

                BattleParticipant.CurrentActionGauge--;
            }
            CurrentDuration--;
            if (CurrentDuration <= 0 || CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                result.Add(new ShowBattleEventParticipantDiesDTO()
                {
                    SourceParticipantId = BattleParticipant.Id,
                    TargetParticipantId = BattleParticipant.Id,
                });
            }
            return result;
        }
        protected ShowBattleEventDTO AutoAttack(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifications)
        {
            target.TakeDamage(CalculateDamage(selfModifications), targetModifications);

            return (new ShowBattleEventNpcAttackDTO()
            {
                SourceParticipantId = BattleParticipant.Id,
                TargetParticipantId = target.Id,
                NewTargetHp = target.CurrentHealth,
            });
        }
        private double CalculateDamage(StatModifications modifications)
        {
            var critPower = _rnd.NextCrit(BattleParticipant.GetCritChance());
            var critMultiplier = Math.Pow(BattleParticipant.GetCriticalMultiplier(), critPower);

            return BattleParticipant.GetDamage() * critMultiplier * modifications.DamageMultiplier;
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

        public void OwnerDeath()
        {
            Side = SideAfterDeath;
        }

        public static BattleParticipant Summon(MinionType minionType, Battle battle, BattleParticipant caster) {
            var entity = minionType switch
            {
                MinionType.BB => new BB(),
                _ => throw new InvalidOperationException("No such minion.")
            };
            var battleParticipant = new BattleParticipant() { };

            entity.Id = Guid.NewGuid();
            entity.Caster = caster;
            entity.CasterId = caster.Id;
            entity.Side = caster.Side;
            entity.BattleParticipant = battleParticipant;
            entity.CurrentHealth = entity.MaxHealth;

            battleParticipant.Id = Guid.NewGuid();
            battleParticipant.Minion = entity;
            battleParticipant.MinionId = entity.Id;
            battleParticipant.Order = caster.Order + 1;
            battle.Participants.Where(x => x.Order > caster.Order).ForEach(x => x.Order++);
            var afterIndex = battle.Participants.TakeWhile(x => x.Id != caster.Id).Count();
            battle.Participants.Insert(afterIndex + 1, battleParticipant);
            battleParticipant.CurrentSpeed = entity.Speed;

            return battleParticipant;
        }
    }

    public class BB : Minion
    {
        public override string Name => "Baszó Béla";

        public override double MaxHealth => 25;

        public override MinionType Type => MinionType.BB;

        public override int? Duration => null;

        public override double Defense => 5;

        public override double CriticalChance => 0.1;

        public override double CriticalMultiplier => 2;

        public override double Damage => 10;
        public override double Speed => 20;
        protected override Side SideAfterDeath => Side.Rogue;

        protected override List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifiers, StatModifications selfModifiers)
        {
            var result = new List<ShowBattleEventDTO>();
            result.Add(AutoAttack(target, targetModifiers, selfModifiers));
            return result;
        }

        protected override BattleParticipant? SelectNonForceTarget(List<BattleParticipant> friendlyParticipants, List<BattleParticipant> opposingParticipants)
        {
            if (opposingParticipants.Count == 0)
            {
                return null;
            }
            var target = _rnd.Next(opposingParticipants);
            return target;
        }
    }

    public class MinionBlueprint
    { 
        public Guid Id { get; set; }
        public MinionType Type { get; set; }
        public Guid SkillId { get; set; }
    }
}
