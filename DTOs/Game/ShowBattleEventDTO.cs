using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowBattleEventDTO
    {
        public Guid TargetParticipantId { get; set; }
        public Guid SourceParticipantId { get; set; }

    }

    public class ShowBattleEventEnemyAttackDTO : ShowBattleEventDTO
    {
        public double NewTargetHp { get; set; }
    }

    public class ShowBattleEventCharacterAttackDTO : ShowBattleEventDTO
    {
        public double NewTargetHp { get; set; }
        public ShowSkillDTO Skill { get; set; }
        public int NewAbilityGauge { get; set; }
        public double NewResource { get; set; }
    }

    public class ShowBattleEventResourceGainDTO : ShowBattleEventDTO
    { 
        public double NewResource { get; set;}
    }

    public class ShowBattleEventSkipTurnDTO : ShowBattleEventDTO 
    { 

    }

    public class ShowBattleEventNextInTurnDTO : ShowBattleEventDTO
    {

    }

    public class ShowBattleEventCombatEndFleeDTO : ShowBattleEventDTO 
    { 

    }

    public class ShowBattleEventAutoAttackDTO : ShowBattleEventDTO
    {
        public double NewTargetHp { get; set; }
        public int NewAbilityGauge { get; set; }
    }
}
