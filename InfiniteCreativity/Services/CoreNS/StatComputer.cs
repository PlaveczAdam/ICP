using InfiniteCreativity.Models.Enums.CoreNS;

namespace InfiniteCreativity.Services.CoreNS
{
    public static class StatComputer
    {
        private static double _baseHealth = 100;
        private static int _baseMovement = 5;
        private static double _baseDamage = 1;
        private static double _baseAbilityDamage = 1;
        private static double _baseAbilityResource = 10;
        private static double _baseAbilityResourceGain = 1;
        private static double _baseCriticalChance = 0.01;
        private static double _baseCriticalMultiplier = 1.5;

        private static Dictionary<Race, double> _raceHealth = new() {
            { Race.Human, 1 },
            { Race.NotHuman, 1.5},
        };
        private static Dictionary<Profession, double> _professionHealth = new() {
            { Profession.Warrior, 2 },
            { Profession.Ranger, 1 },
            { Profession.Mage, 0.5 },
            { Profession.Support, 0.75 },
        };

        private static Dictionary<Race, int> _raceMovement = new() {
            { Race.Human, 0 },
            { Race.NotHuman, 1 },
        };
        private static Dictionary<Profession, int> _professionMovement = new(){
            { Profession.Warrior, -1 },
            { Profession.Ranger, 1 },
            { Profession.Mage, 0 },
            { Profession.Support, 0 },
        };

        private static Dictionary<Race, double> _raceDamage = new() {
            { Race.Human, 1.1 },
            { Race.NotHuman, 0.8},
        };
        private static Dictionary<Profession, double> _professionDamage = new() {
            { Profession.Warrior, 1.5 },
            { Profession.Ranger, 1.25 },
            { Profession.Mage, 0.25 },
            { Profession.Support, 0.75 },
        };

        private static Dictionary<Race, double> _raceAbilityDamage = new() {
            { Race.Human, 1 },
            { Race.NotHuman, 1.2},
        };
        private static Dictionary<Profession, double> _professionAbilityDamage = new() {
            { Profession.Warrior, 0.25 },
            { Profession.Ranger, 0.75 },
            { Profession.Mage, 1.5 },
            { Profession.Support, 1.25 },
        };

        private static Dictionary<Race, double> _raceAbilityResource = new() {
            { Race.Human, 1 },
            { Race.NotHuman, 1.2},
        };
        private static Dictionary<Profession, double> _professionAbilityResource = new() {
            { Profession.Warrior, 0.25 },
            { Profession.Ranger, 0.75 },
            { Profession.Mage, 1.5 },
            { Profession.Support, 1.25 },
        };

        private static Dictionary<Race, double> _raceAbilityResourceGain = new() {
            { Race.Human, 0.75 },
            { Race.NotHuman, 1.2},
        };
        private static Dictionary<Profession, double> _professionAbilityResourceGain = new() {
            { Profession.Warrior, 0.25 },
            { Profession.Ranger, 0.75 },
            { Profession.Mage, 1.5 },
            { Profession.Support, 1.25 },
        };

        private static Dictionary<Profession, double> _professionCriticalChance = new() {
            { Profession.Warrior, 0.075 },
            { Profession.Ranger, 0.2 },
            { Profession.Mage, 0.1 },
            { Profession.Support, 0.05 },
        };

        private static Dictionary<Profession, double> _professionCriticalMultiplier = new() {
            { Profession.Warrior, 1.25 },
            { Profession.Ranger, 1.5 },
            { Profession.Mage, 1.5 },
            { Profession.Support, 1.25 },
        };

        private static Dictionary<Profession, double> _professionSpeed = new() {
            { Profession.Warrior, 0.9 },
            { Profession.Ranger, 1.2 },
            { Profession.Mage, 1 },
            { Profession.Support, 1 },
        };

        public static double ComputeBaseHealth(Race race, Profession profession, double level)
        {
            return _baseHealth * level * _raceHealth[race] * _professionHealth[profession];
        }
        public static int ComputeBaseMovement(Race race, Profession profession)
        {
            return _baseMovement + _raceMovement[race] + _professionMovement[profession];
        }
        public static double ComputeBaseDamage(Race race, Profession profession, double level)
        {
            return (_baseDamage + level) * _raceDamage[race] * _professionDamage[profession];
        }
        public static double ComputeBaseAbilityDamage(Race race, Profession profession, double level)
        {
            return (_baseAbilityDamage + level) * _raceAbilityDamage[race] * _professionAbilityDamage[profession];
        }
        internal static double ComputeBaseAbilityResource(Race race, Profession profession)
        {
            return _baseAbilityResource * _raceAbilityResource[race] * _professionAbilityResource[profession];
        }
        internal static double ComputeBaseAbilityResourceGain(Race race, Profession profession)
        {
            return _baseAbilityResourceGain * _raceAbilityResourceGain[race] * _professionAbilityResourceGain[profession];
        }

        internal static double ComputeBaseCriticalChance(Profession profession)
        {
            return _professionCriticalChance[profession];
        }

        internal static double ComputeBaseCriticalMultiplier(Profession profession)
        {
            return _professionCriticalMultiplier[profession];
        }

        internal static double ComputeBaseSpeed(Profession profession)
        {
            return _professionSpeed[profession];
        }
    }
}
