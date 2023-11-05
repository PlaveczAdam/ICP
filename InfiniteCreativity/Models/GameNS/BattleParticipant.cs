using AutoMapper;
using DTOs.Enums.GameNS;
using DTOs.Game;
using InfiniteCreativity.Models.GameNS.Enemys;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace InfiniteCreativity.Models.CoreNS
{
    public class BattleParticipant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Character? Character { get; set; }
        public Enemy? Enemy { get; set; }
        public Minion? Minion { get; set; }
        public double CurrentSpeed { get; set; }
        public int Order { get; set; }
        public int CurrentActionGauge { get; set; }
        public List<Minion> OwnedMinions { get; set; }
        [NotMapped]
        public int ActionGauge => (int)CurrentSpeed / 10;
        public List<Buff> Buffs { get; set; } = new List<Buff>();
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public Guid? EnemyId { get; set; }
        public Guid? MinionId { get; set; }
        public Guid? CharacterId { get; set; }
        [NotMapped]
        public bool IsAlive => CurrentHealth > 0;
        [NotMapped]
        public Side Side { get {
                if (Character is not null)
                {
                    return Side.Player;
                }
                else if (Enemy is not null)
                {
                    return Side.Enemy;
                }
                else
                {
                    return Minion.Side;
                }
            } 
        }

        public StatModifications CalculateStatModifications()
        {
            var buffModifications = Buffs.Where(x => x is PassiveBuff).Cast<PassiveBuff>().Aggregate(new StatModifications(), (acc, curr) => acc.Merge(curr.StatModifications));
            var conditionModifications = Conditions.Where(x => x is PassiveCondition).Cast<PassiveCondition>().Aggregate(new StatModifications(), (acc, curr) => acc.Merge(curr.StatModifications));
            return buffModifications.Merge(conditionModifications);
        }

        public void TakeDamage(double damage, StatModifications modifications)
        {
            if (Enemy is not null)
            {
                Enemy.TakeDamage(damage, modifications);
            }
            else if (Character is not null)
            {
                Character.TakeDamage(damage, modifications);
            }
            else
            {
                Minion.TakeDamage(damage, modifications);
            }
        }

        public void TakeConditionDamage(double damage)
        {
            if (Enemy is not null)
            {
                Enemy.TakeConditionDamage(damage);
            }
            else if (Character is not null)
            {
                Character.TakeConditionDamage(damage);
            }
            else
            {
                Minion.TakeConditionDamage(damage);
            }
        }

        public double GetCritChance()
        {
            if (Enemy is not null)
            {
                return Enemy.CriticalChance;
            }
            else if (Character is not null)
            {
                return Character.CriticalChance;
            } else
            {
                return Minion.CriticalChance;
            }
        }
        public double GetCriticalMultiplier()
        {
            if (Enemy is not null)
            {
                return Enemy.CriticalMultiplier;
            }
            else if (Character is not null)
            {
                return Character.CriticalMultiplier;
            } else
            {
                return Minion.CriticalMultiplier;
            }
        }
        public double GetDamage()
        {
            if (Enemy is not null)
            {
                return Enemy.Damage;
            }
            else if (Character is not null)
            {
                return Character.Damage;
            }
            else
            {
                return Minion.Damage;
            }
        }
        public double GetDefense()
        {
            if (Enemy is not null)
            {
                return Enemy.Defense;
            }
            else if (Character is not null)
            {
                return Character.Defense;
            }
            else
            {
                return Minion.Defense;
            }
        }

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> battleParticipants, IMapper mapper)
        {
            if (Enemy is not null)
            {
                return Enemy.Turn(battleParticipants, mapper);
            }
            else if (Character is not null)
            {
                throw new InvalidOperationException("Player has no such action.");
            }
            else
            {
                return Minion.Turn(battleParticipants, mapper);
            }
        }
        [NotMapped]
        public double CurrentHealth {
            get {
                return Enemy?.Health ?? Character?.CurrentHealth ?? Minion!.CurrentHealth;
            }
            set {
                var adjustedHealth = Math.Min(Health, value);
                if (Enemy is not null)
                {
                    Enemy.Health = adjustedHealth;
                }
                else if (Character is not null)
                {
                    Character.CurrentHealth = adjustedHealth;
                }
                else
                {
                    Minion.CurrentHealth = adjustedHealth;
                }
            }
        }
        public double Health => Enemy?.MaxHealth ?? Character?.Health ?? Minion!.MaxHealth;
    }
}
