using DTOs.Enums.CoreNS;
using DTOs.Enums.GameNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public class ConditionBlueprint
    {
        public Guid ID { get; set; }
        public ConditionType ConditionType { get; set; }
        public int Duration { get; set; }
        public Guid SkillId { get; set; }
    }
}
