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

    public class ShowBattleEventCharacterHealDTO : ShowBattleEventDTO
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

    public class ShowBattleEventParticipantDiesDTO : ShowBattleEventDTO
    {

    }

    public class ShowBattleEventCombatEndDefeatDTO : ShowBattleEventDTO
    {

    }

    public class ShowBattleEventCombatEndVictoryDTO : ShowBattleEventDTO
    { 

    }

    public class ShowBattleEventApplyBuffDTO : ShowBattleEventDTO
    {
        public ShowBuffDTO Buff { get; set; }
    }

    public class ShowBattleEventRejuvenationTickDTO : ShowBattleEventDTO
    {
        public double NewAbilityResource { get; set; }
        public ShowBuffDTO Buff { get; set; }
    }

    public class ShowBattleEventBuffExpiredDTO : ShowBattleEventDTO
    {
        public ShowBuffDTO Buff { get; set; }
    }

    public class ShowBattleEventBleedTickDTO : ShowBattleEventDTO
    {
        public ShowConditionDTO Condition { get; set; }
        public double NewTargetHealth { get; set; }
    }
    public class ShowBattleEventApplyConditionDTO : ShowBattleEventDTO
    {
        public ShowConditionDTO Condition { get; set; }
    }
    public class ShowBattleEventConditionExpiredDTO : ShowBattleEventDTO
    {
        public ShowConditionDTO Condition { get; set; }
    }
}
