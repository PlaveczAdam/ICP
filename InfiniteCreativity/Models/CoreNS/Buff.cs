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
        public BattleParticipant Caster { get; set; } 
        public Guid BattleParticipantId { get; set; }
        public Guid CasterId { get; set; }
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

    public class Regeneration : Buff
    {
        public override string Name => "Regeneration";

        public override string Description => "Regenerates health";

        public override BuffType BuffType => BuffType.Regeneration;

        public override bool StacksDuration => true;

        public override ShowBattleEventDTO Tick(IMapper mapper)
        {
            Duration--;
            BattleParticipant.CurrentHealth += BattleParticipant.Health * 0.05;

            return new ShowBattleEventRegenerationTickDTO()
            {
                SourceParticipantId = BattleParticipant.Id,
                TargetParticipantId = BattleParticipant.Id,
                Buff = mapper.Map<ShowBuffDTO>(this),
                NewHealth = BattleParticipant.CurrentHealth,
            };
        }
    }

    public class StatModifications
    {
        public double DamageMultiplier { get; set; } = 1;
        public double DefenseMultiplier { get; set; } = 1;
        public StatModifications Merge(StatModifications other) {
            return new StatModifications() { DamageMultiplier = DamageMultiplier * other.DamageMultiplier, DefenseMultiplier = DefenseMultiplier * other.DefenseMultiplier};
        }
    }

    public abstract class PassiveBuff : Buff
    { 
        public abstract StatModifications StatModifications { get; }
        public override ShowBattleEventDTO Tick(IMapper mapper)
        {
            Duration--;
            return new ShowBattleEventBuffTickDTO() {
                SourceParticipantId = BattleParticipant.Id,
                TargetParticipantId = BattleParticipant.Id,
                Buff = mapper.Map<ShowBuffDTO>(this),
            };
        }
    }

    public class Might : PassiveBuff
    {
        public override string Name => "Might";

        public override string Description => "stronk";

        public override BuffType BuffType => BuffType.Might;

        public override bool StacksDuration => false;
        public override StatModifications StatModifications => new StatModifications { 
            DamageMultiplier = 1.05
        };
    }
    public class DefenseUp : PassiveBuff
    {
        public override string Name => "DefenseUp";

        public override string Description => "stronk";

        public override BuffType BuffType => BuffType.DefenseUp;

        public override bool StacksDuration => true;
        public override StatModifications StatModifications => new StatModifications
        {
            DefenseMultiplier = 1.3
        };
    }
}
