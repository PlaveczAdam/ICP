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
    }
}
