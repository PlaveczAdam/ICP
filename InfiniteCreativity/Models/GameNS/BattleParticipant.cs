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
