using InfiniteCreativity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowBattleStateDTO
    {
        public Guid Id { get; set; }
        public List<ShowBattleParticipantDTO> Participants { get; set; }
        public bool HasStarted { get; set; }
        public List<ShowBattleEventDTO> BattleEvents { get; set; } = new();
        public ICollection<Guid> TurnPredictions { get; set; }
        public ShowBattleParticipantDTO NextInTurn { get; set; }
    }
}
