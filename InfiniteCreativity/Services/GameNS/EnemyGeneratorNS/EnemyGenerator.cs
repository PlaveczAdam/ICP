using DTOs.Enums.CoreNS;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Models.GameNS.Enemys;

namespace InfiniteCreativity.Services.GameNS.EnemyGeneratorNS
{
    public class EnemyGenerator
    {
        private Random _rnd = new Random();
        private List<string> nameList = new List<string>() {
            "Beanos",
            "Bruh",
            "OutOfIdeas",
            "Garlic",
            "RadarChart",
            "BigChonker"
        };

        public Enemy Generate(double level, bool? isBoss = null, EnemyType? type = null)
        {
            Enemy enemy;
            var isboss = isBoss ?? _rnd.Next(3) == 0;
            if (isboss)
            {
                enemy = new Boss();
            }
            else
            {
                enemy = new Enemy();
            }

            enemy.Level = _rnd.NextDouble(Math.Max(level - 5, 1), level + 5);
            enemy.EnemyType = type ?? _rnd.Next(Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToList());
            enemy.Health = enemy.MaxHealth;
            enemy.BehaviourType = _rnd.Next<EnemyBehaviourType>();

            switch (enemy)
            {
                case Boss boss:
                    return GenerateBoss(boss);
                default:
                    return enemy;
            }
        }
        private Boss GenerateBoss(Boss boss)
        {
            boss.Name = _rnd.Next(nameList);
            return boss;
        }
    }
}
