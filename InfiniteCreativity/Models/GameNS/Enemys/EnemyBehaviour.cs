using DTOs.Enums.CoreNS;
using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using static System.Net.Mime.MediaTypeNames;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public abstract class EnemyBehaviour
    {
        private BattleParticipant _selfParticipant;
        protected Random _rnd = new Random();

        protected EnemyBehaviour(BattleParticipant selfParticipant)
        {
            _selfParticipant = selfParticipant;
        }

        public static EnemyBehaviour Create(BattleParticipant selfParticipant)
        {
            return selfParticipant.Enemy.BehaviourType switch
            {
                EnemyBehaviourType.TonkKiller => new TonkKiller(selfParticipant),
                EnemyBehaviourType.Assassin => new Assassin(selfParticipant)
            };
        }

        public BattleParticipant? SelectTarget(List<BattleParticipant> aliveFriendly, List<BattleParticipant> aliveEnemy, BattleParticipant? forceTarget)
        {
            if (forceTarget is not null)
            {
                return forceTarget;
            }else
            {
                return SelectNonForcedTarget(aliveFriendly, aliveEnemy);
            }
        }

        protected abstract BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> aliveFriendly, List<BattleParticipant> aliveEnemy);
        public abstract List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifitaions);
        protected ShowBattleEventDTO AutoAttack(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifications)
        {
            target.TakeDamage(CalculateDamage(selfModifications), targetModifications);

            return (new ShowBattleEventNpcAttackDTO()
            {
                SourceParticipantId = _selfParticipant.Id,
                TargetParticipantId = target.Id,
                NewTargetHp = target.CurrentHealth
            });
        }
        private double CalculateDamage(StatModifications modifications)
        {
            var critPower = _rnd.NextCrit(_selfParticipant.GetCritChance());
            var critMultiplier = Math.Pow(_selfParticipant.GetCriticalMultiplier(), critPower);

            return _selfParticipant.GetDamage() * critMultiplier * modifications.DamageMultiplier;
        }
    }

    public class TonkKiller : EnemyBehaviour
    {
        public TonkKiller(BattleParticipant selfParticipant) : base(selfParticipant)
        {
        }

        public override List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifitaions)
        {
            List<ShowBattleEventDTO> result = new List<ShowBattleEventDTO>();
            result.Add(AutoAttack(target, targetModifications, selfModifitaions));
            return result;
        }

        protected override BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> aliveFriendly, List<BattleParticipant> aliveEnemy)
        {
            var target = aliveEnemy.MaxBy(x => x.GetDefense());
            return target;
        }
    }

    public class Assassin : EnemyBehaviour
    {
        public Assassin(BattleParticipant selfParticipant) : base(selfParticipant)
        {
        }
        public override List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifitaions)
        {
            List<ShowBattleEventDTO> result = new List<ShowBattleEventDTO>();
            result.Add(AutoAttack(target, targetModifications, selfModifitaions));
            return result;
        }
        protected override BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> aliveFriendly, List<BattleParticipant> aliveEnemy)
        {
            var target = aliveEnemy.MinBy(x => x.GetDefense());
            return target;
        }
    }
}
