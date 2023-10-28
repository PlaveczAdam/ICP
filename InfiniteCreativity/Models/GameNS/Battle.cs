using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Models.GameNS
{
    public class Battle
    {
        public Guid Id { get; set; }
        public ICollection<BattleParticipant> Participants { get; set; }
        public bool HasStarted { get; set; } = false;
        public BattleParticipant? NextInTurn { get; set; }
        public Guid? NextInTurnId { get; set; }
    }
}
