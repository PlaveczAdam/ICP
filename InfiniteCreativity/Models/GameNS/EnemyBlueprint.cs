using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Models.GameNS
{
    public class EnemyBlueprint
    {
        public Guid Id { get; set; }
        public bool IsBoss { get; set; }
        public EnemyType Type { get; set; }
    }
}
