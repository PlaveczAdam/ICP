using AutoMapper;
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

        private double _health;
        public double Health { get => _health; 
            set {
                if (_health > 0 && value <= 0)
                {
                    Die();
                }
                _health = value;
            } 
        }

        private void Die()
        {
            BattleParticipant.Buffs.Clear();
            BattleParticipant.Conditions.Clear();
            BattleParticipant.OwnedMinions.ForEach(x => x.OwnerDeath());

        }

        public GConnection GConnection { get; set; }
        public HexTileDataObject? Tile { get; set; }
        public EnemyBehaviourType BehaviourType { get; set; }

        [NotMapped]
        public virtual string Name => EnemyType.ToString();
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
        [NotMapped]
        public EnemyBehaviour? EnemyBehaviour
        {
            get
            {
                if (_enemyBehaviour is null && this.BattleParticipant is not null)
                {
                    _enemyBehaviour = EnemyBehaviour.Create(BattleParticipant);
                }
                return _enemyBehaviour;
            }
            set => _enemyBehaviour = value;
        }

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> allParticipant, IMapper mapper)
        {
            var result = new List<ShowBattleEventDTO>();
            var forceTarget = BattleParticipant.Conditions.FirstOrDefault(x => x is Taunt)?.Caster;

            while (BattleParticipant.CurrentActionGauge > 0)
            {
                var aliveParticipants = allParticipant.Where(x => x.IsAlive).ToList();
                var aliveFriendly = aliveParticipants.Where(x => x.Side == BattleParticipant.Side).ToList();
                var aliveEnemy = aliveParticipants.Where(x => x.Side != BattleParticipant.Side).ToList();
                var selfModifiers = BattleParticipant.CalculateStatModifications();

                if (!forceTarget?.IsAlive ?? false)
                {
                    forceTarget = null;
                }

                var target = EnemyBehaviour.SelectTarget(aliveFriendly, aliveEnemy, forceTarget);

                if (target is null)
                {
                    return result;
                }

                var targetModifiers = target.CalculateStatModifications();

                result.AddRange(EnemyBehaviour.ActionTurn(target, targetModifiers, selfModifiers));

                if (!target.IsAlive)
                {
                    result.Add(new ShowBattleEventParticipantDiesDTO()
                    {
                        SourceParticipantId = BattleParticipant.Id,
                        TargetParticipantId = target.Id,
                        MinionsChangingSide = mapper.Map<List<ShowBattleParticipantDTO>>(
                        target.OwnedMinions
                            .Where(x => x.Side != target.Side)
                            .Select(x => x.BattleParticipant)),
                    });
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
        }
    }
}
