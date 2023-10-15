using AutoMapper;
using DTOs.Enums.CoreNS;
using DTOs.Enums.GameNS;
using DTOs.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public abstract class Condition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }
        [NotMapped]
        public abstract string Name { get; }
        [NotMapped]
        public abstract string Description { get; }
        public int Duration { get; set; }
        [NotMapped]
        public abstract ConditionType ConditionType { get; }
        public double ConditionDamageMultiplier { get; set; }

        public abstract bool StacksDuration { get; }

        public abstract List<ShowBattleEventDTO> Tick(IMapper mapper);
        public BattleParticipant BattleParticipant { get; set; }
    }

    public class Bleed : Condition
    {
        public override string Name => "Bleed";

        public override string Description => "meh.";

        public override ConditionType ConditionType => ConditionType.Bleed;

        public override bool StacksDuration => false;

        public override List<ShowBattleEventDTO> Tick(IMapper mapper)
        {
            var result = new List<ShowBattleEventDTO>();
            Duration--;

            if (BattleParticipant.Enemy is not null)
            {
                BattleParticipant.Enemy.TakeConditionDamage(2 * ConditionDamageMultiplier);
            }
            else
            {
                BattleParticipant.Character.TakeConditionDamage(2 * ConditionDamageMultiplier);
            }

            result.Add(new ShowBattleEventBleedTickDTO()
            {
                SourceParticipantId = BattleParticipant.Id,
                TargetParticipantId = BattleParticipant.Id,
                Condition = mapper.Map<ShowConditionDTO>(this),
                NewTargetHealth = BattleParticipant.GetCurrentHealth(),
            });

            if (BattleParticipant.GetCurrentHealth() <= 0)
            {
                result.Add(new ShowBattleEventParticipantDiesDTO()
                {
                    SourceParticipantId = BattleParticipant.Id,
                    TargetParticipantId = BattleParticipant.Id,
                });
                BattleParticipant.Conditions.Clear();
            };

            return result;
        }

    }
}
