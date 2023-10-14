using DTOs.Enums.CoreNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public class BuffBlueprint
    {
        public Guid ID { get; set; }
        public BuffType BuffType { get; set; }
        public int Duration { get; set; }
        public Guid SkillId { get; set; }
    }
}
