using AutoMapper;
using DTOs.Enums.CoreNS;
using DTOs.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public abstract class Buff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }
        [NotMapped]
        public abstract string Name { get; }
        [NotMapped]
        public abstract string Description { get; }
        public int Duration { get; set; }
        [NotMapped]
        public abstract BuffType BuffType { get; }

        public abstract bool StacksDuration { get; }

        public abstract ShowBattleEventDTO Tick(IMapper mapper);
        public BattleParticipant BattleParticipant { get; set; }
    }

    public class Rejuvenation : Buff
    {
        public override string Name => "Rejuvenation";

        public override string Description => "Regenerates ability resource.";

        public override BuffType BuffType => BuffType.Rejuvenation;

        public override bool StacksDuration => true;

        public override ShowBattleEventDTO Tick(IMapper mapper)
        {
            Duration--;
            BattleParticipant.Character.CurrentAbilityResource += BattleParticipant.Character.AbilityResource * 0.05;
            if (BattleParticipant.Character.CurrentAbilityResource > BattleParticipant.Character.AbilityResource)
            {
                BattleParticipant.Character.CurrentAbilityResource = BattleParticipant.Character.AbilityResource;
            }

            return new ShowBattleEventRejuvenationTickDTO()
            {
                NewAbilityResource = BattleParticipant.Character.CurrentAbilityResource,
                SourceParticipantId = BattleParticipant.Id,
                TargetParticipantId = BattleParticipant.Id,
                Buff = mapper.Map<ShowBuffDTO>(this),
            };
        }
    }
}
