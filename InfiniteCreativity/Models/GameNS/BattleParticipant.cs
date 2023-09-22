using InfiniteCreativity.Models.GameNS.Enemys;

namespace InfiniteCreativity.Models.CoreNS
{
    public class BattleParticipant
    {
        public Guid Id { get; set; }
        public Character? Character { get; set; }
        public Enemy? Enemy { get; set; }
        public double CurrentSpeed { get; set; }
    }
}
