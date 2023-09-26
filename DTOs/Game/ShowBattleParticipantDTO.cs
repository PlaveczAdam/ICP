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
        public double CurrentSpeed { get; set; }

    }
}
