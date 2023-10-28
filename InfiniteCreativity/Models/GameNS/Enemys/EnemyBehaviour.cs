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

        public BattleParticipant? SelectTarget(List<BattleParticipant> alivePlayers, List<BattleParticipant> aliveEnemys, BattleParticipant? forceTarget)
        {
            if (forceTarget is not null)
            {
                return forceTarget;
            }else
            {
                return SelectNonForcedTarget(alivePlayers, aliveEnemys);
            }
        }

        protected abstract BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> alivePlayers, List<BattleParticipant> aliveEnemys);
        public abstract List<ShowBattleEventDTO> ActionTurn(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifitaions);
        protected ShowBattleEventDTO AutoAttack(BattleParticipant target, StatModifications targetModifications, StatModifications selfModifications)
        {
            target!.Character!.TakeDamage(CalculateDamage(selfModifications), targetModifications);

            return (new ShowBattleEventEnemyAttackDTO()
            {
                SourceParticipantId = _selfParticipant.Id,
                TargetParticipantId = target.Id,
                NewTargetHp = target.Character.CurrentHealth
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

        protected override BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> alivePlayers, List<BattleParticipant> aliveEnemys)
        {
            var target = alivePlayers.MaxBy(x => x.Character.Defense);
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
        protected override BattleParticipant? SelectNonForcedTarget(List<BattleParticipant> alivePlayers, List<BattleParticipant> aliveEnemys)
        {
            var target = alivePlayers.MinBy(x => x.Character.Defense);
            return target;
        }
    }
}
