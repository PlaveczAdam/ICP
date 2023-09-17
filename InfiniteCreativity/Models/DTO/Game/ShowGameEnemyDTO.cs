using InfiniteCreativity.Models.Enums;
using InfiniteCreativity.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.DTO.Game
{
    public class ShowGameEnemyDTO
    {
        public int Id { get; set; }
        public double Level { get; set; }
        public EnemyType EnemyType { get; set; }

        public double Health { get; set; }
        public GConnection GConnection { get; set; }
        public string Name { get; set; }
        public virtual double MaxHealth { get; set; }
        public double Defense { get; set; }
        public double Damage { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
    }
}
