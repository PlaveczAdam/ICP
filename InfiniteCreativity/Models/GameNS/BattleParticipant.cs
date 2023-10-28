using InfiniteCreativity.Models.GameNS.Enemys;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public class BattleParticipant
    {
        public Guid Id { get; set; }
        public Character? Character { get; set; }
        public Enemy? Enemy { get; set; }
        public double CurrentSpeed { get; set; }
        public int Order { get; set; }
        public int CurrentActionGauge { get; set; }
        [NotMapped]
        public int ActionGauge => (int)CurrentSpeed / 10;
        public List<Buff> Buffs { get; set; } = new List<Buff>();
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public Guid? EnemyId { get; set; }
        [NotMapped]
        public bool IsAlive => GetCurrentHealth() > 0;

        public StatModifications CalculateStatModifications()
        {
            var buffModifications = Buffs.Where(x => x is PassiveBuff).Cast<PassiveBuff>().Aggregate(new StatModifications(), (acc, curr) => acc.Merge(curr.StatModifications));
            var conditionModifications = Conditions.Where(x => x is PassiveCondition).Cast<PassiveCondition>().Aggregate(new StatModifications(), (acc, curr) => acc.Merge(curr.StatModifications));
            return buffModifications.Merge(conditionModifications);
        }
        public double GetCritChance()
        {
            if (Enemy is not null)
            {
                return Enemy.CriticalChance;
            }
            else
            {
                return Character.CriticalChance;
            }
        }
        public double GetCriticalMultiplier()
        {
            if (Enemy is not null)
            {
                return Enemy.CriticalMultiplier;
            }
            else
            {
                return Character.CriticalMultiplier;
            }
        }
        public double GetDamage()
        {
            if (Enemy is not null)
            {
                return Enemy.Damage;
            }
            else
            {
                return Character.Damage;
            }
        }

        public double GetCurrentHealth() {
            if (Enemy is not null)
            {
                return Enemy.Health;
            }
            else
            {
                return Character.CurrentHealth;
            }
        }
    }
}
