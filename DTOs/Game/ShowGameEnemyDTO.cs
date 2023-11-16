using DTOs.Enums.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.DTO.Game
{
    public class ShowGameEnemyDTO
    {
        public Guid Id { get; set; }
        public double Level { get; set; }
        public EnemyType EnemyType { get; set; }

        public double Health { get; set; }
        public string Name { get; set; }
        public virtual double MaxHealth { get; set; }
        public double Defense { get; set; }
        public double Damage { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
        public EnemyBehaviourType BehaviourType { get; set; }

    }
}
