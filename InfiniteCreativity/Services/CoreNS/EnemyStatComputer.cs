using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public static class EnemyStatComputer
    {
        private static Dictionary<EnemyType, double> _enemyTypeHealth = new() {
            { EnemyType.Slime, 50},
        };

        private static Dictionary<EnemyType, double> _enemyTypeDefense = new() {
             { EnemyType.Slime, 1},
        };

        private static Dictionary<EnemyType, double> _enemyTypeDamage = new() {
            { EnemyType.Slime, 1},
        };

        private static Dictionary<EnemyType, double> _enemyTypeCriticalChance = new() {
            { EnemyType.Slime, 0.1},
        };

        private static Dictionary<EnemyType, double> _enemyTypeCriticalMultiplier = new() {
            { EnemyType.Slime, 1.5},
        };

        private static Dictionary<EnemyType, double> _enemyTypeSpeed = new() {
            { EnemyType.Slime, 20},
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

        internal static double CalculateSpeed(EnemyType enemyType)
        {
            return _enemyTypeSpeed[enemyType];
        }
    }
}
