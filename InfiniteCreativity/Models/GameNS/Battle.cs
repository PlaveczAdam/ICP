using InfiniteCreativity.Models.CoreNS;

namespace InfiniteCreativity.Models.GameNS
{
    public class Battle
    {
        public Guid Id { get; set; }
        public ICollection<BattleParticipant> Participants { get; set; }

    }
}
