using InfiniteCreativity.DTO.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowBattleParticipantDTO
    {
        public Guid Id { get; set; }
        public ShowGameCharacterDTO? Character { get; set; }
        public ShowGameEnemyDTO? Enemy { get; set; }
        public int Order { get; set; }
        public int CurrentActionGauge { get; set; }
        public int ActionGauge { get; set; }
        public double CurrentSpeed { get; set; }
        public List<ShowBuffDTO> Buffs { get; set;}

    }
}
