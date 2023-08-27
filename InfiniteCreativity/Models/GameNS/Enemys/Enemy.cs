using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Enemy
    {
        public int Id { get; set; }
        public double Level { get; set; }
        public EnemyType EnemyType { get; set; }

        public double Health { get; set; }
        public GConnection GConnection { get; set; }

        [NotMapped]
        public string Name => EnemyType.ToString();
        [NotMapped]
        public virtual double MaxHealth => EnemyStatComputer.CalculateMaxHealth(Level, EnemyType);
        [NotMapped]
        public double Defense => EnemyStatComputer.CalculateDefense(Level, EnemyType);
        [NotMapped]
        public double Damage => EnemyStatComputer.CalculateDamage(Level, EnemyType);
        [NotMapped]
        public double CriticalChance => EnemyStatComputer.CalculateCriticalChance(Level, EnemyType);
        [NotMapped]
        public double CriticalMultiplier => EnemyStatComputer.CalculateCriticalMultiplier(Level, EnemyType);

    }
}
