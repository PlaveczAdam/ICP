using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public static class EnemyStatComputer
    {
        private static Dictionary<EnemyType, double> _enemyTypeHealth = new() {
            { EnemyType.Bean, 150 },
            { EnemyType.Trunk, 200},
        };

        private static Dictionary<EnemyType, double> _enemyTypeDefense = new() {
            { EnemyType.Bean, 1 },
            { EnemyType.Trunk, 2},
        };

        private static Dictionary<EnemyType, double> _enemyTypeDamage = new() {
            { EnemyType.Bean, 5 },
            { EnemyType.Trunk, 10},
        };

        private static Dictionary<EnemyType, double> _enemyTypeCriticalChance = new() {
            { EnemyType.Bean, 0.05 },
            { EnemyType.Trunk, 0.1},
        };

        private static Dictionary<EnemyType, double> _enemyTypeCriticalMultiplier = new() {
            { EnemyType.Bean, 1.5 },
            { EnemyType.Trunk, 1.75},
        };

        internal static double CalculateCriticalChance(double level, EnemyType enemyType)
        {
            return _enemyTypeCriticalChance[enemyType] * Math.Sqrt(level);
        }

        internal static double CalculateCriticalMultiplier(double level, EnemyType enemyType)
        {
            return _enemyTypeCriticalMultiplier[enemyType] * Math.Pow(level, 1 / 5d);
        }

        internal static double CalculateDamage(double level, EnemyType enemyType)
        {
            return _enemyTypeDamage[enemyType] * Math.Pow(level, 1 / 5d);
        }

        internal static double CalculateDefense(double level, EnemyType enemyType)
        {
            return _enemyTypeDefense[enemyType] * Math.Pow(level, 1 / 5d);
        }

        internal static double CalculateMaxHealth(double level, EnemyType enemyType)
        {
            return _enemyTypeHealth[enemyType] * level;
        }
    }
}
