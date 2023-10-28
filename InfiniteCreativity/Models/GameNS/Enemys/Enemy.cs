using DTOs.Enums.CoreNS;
using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Enemy
    {
        private Random _rnd = new Random();

        public Guid Id { get; set; }
        public double Level { get; set; }
        public EnemyType EnemyType { get; set; }

        public double Health { get; set; }
        public GConnection GConnection { get; set; }
        public HexTileDataObject? Tile { get; set; }
        public EnemyBehaviourType BehaviourType { get; set; }

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
        [NotMapped]
        public double Speed => EnemyStatComputer.CalculateSpeed(EnemyType);
        [NotMapped]
        private EnemyBehaviour? _enemyBehaviour;
        public BattleParticipant? BattleParticipant { get; set; }

        public Enemy(BattleParticipant? battleParticipant)
        {
            BattleParticipant = battleParticipant;
            if (battleParticipant is not null)
            {
                _enemyBehaviour = EnemyBehaviour.Create(BattleParticipant);
            }
        }

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> characterParticipants, List<BattleParticipant> enemyParticipants)
        {
            var result = new List<ShowBattleEventDTO>();
            var forceTarget = BattleParticipant.Conditions.FirstOrDefault(x => x is Taunt)?.Caster;

            while (BattleParticipant.CurrentActionGauge > 0)
            {
                var alivePlayers = characterParticipants.Where(x => x.IsAlive).ToList();
                var aliveEnemys = enemyParticipants.Where(x => x.IsAlive).ToList();
                var selfModifiers = BattleParticipant.CalculateStatModifications();

                if (forceTarget?.GetCurrentHealth() <= 0)
                {
                    forceTarget = null;
                }

                if (alivePlayers.Count() == 0)
                {
                    return result;
                }

                var target = _enemyBehaviour.SelectTarget(alivePlayers, aliveEnemys, forceTarget);
                var targetModifiers = target.CalculateStatModifications();

                result.AddRange(_enemyBehaviour.ActionTurn(target, targetModifiers, selfModifiers));

                if (!target.IsAlive)
                {
                    result.Add(new ShowBattleEventParticipantDiesDTO()
                    {
                        SourceParticipantId = BattleParticipant.Id,
                        TargetParticipantId = target.Id,
                    });
                    target.Buffs.Clear();
                    target.Conditions.Clear();
                }

                BattleParticipant.CurrentActionGauge--;
            }

            return result;
        }

        public void TakeDamage(double damage, StatModifications modifications)
        {
            Health -= Math.Max(damage - (Defense * modifications.DefenseMultiplier), 0);
        }

        public void TakeConditionDamage(double cDamage)
        {
            Health -= Math.Max(cDamage, 0);

            if (Health <= 0)
            {
                Health = 0;
            }
        }
    }
}
