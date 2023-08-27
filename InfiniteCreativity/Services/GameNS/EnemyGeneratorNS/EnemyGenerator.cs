using InfiniteCreativity.Models.ArmorNs;
using InfiniteCreativity.Models.Materials;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Models;
using InfiniteCreativity.Services.ItemGeneratorNS;
using System;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.Enums;
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

        public Enemy Generate(double level)
        {
            Enemy enemy;
            var isboss = _rnd.Next(3) == 0;
            if (isboss)
            {
                enemy = new Boss();
            }
            else
            {
                enemy = new Enemy();
            }
            enemy.Level = _rnd.NextDouble(Math.Max(level - 5, 1), level + 5);
            enemy.EnemyType = _rnd.Next(Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToList());

            switch (enemy)
            {
                case Boss boss:
                    return GenerateBoss(boss);
                default:
                    return enemy;
            }
        }
        public Boss GenerateBoss(Boss boss)
        {
            boss.Name = _rnd.Next(nameList);
            return boss;
        }
    }
}
