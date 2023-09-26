using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;
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

        public List<ShowBattleEventDTO> Turn(List<BattleParticipant> characterParticipants, BattleParticipant selfParticipant)
        {
            var result = new List<ShowBattleEventDTO>();
            while (selfParticipant.CurrentActionGauge > 0)
            {
                var target = characterParticipants.MaxBy(x => x.Character!.Defense);
                target!.Character!.TakeDamage(CalculateDamage());

                result.Add(new ShowBattleEventEnemyAttackDTO()
                {
                    SourceParticipantId = selfParticipant.Id,
                    TargetParticipantId = target.Id,
                    NewTargetHp = target.Character.CurrentHealth
                });

                selfParticipant.CurrentActionGauge--;
            }

            return result;
        }
        public double CalculateDamage()
        {
            var critPower = _rnd.NextCrit(CriticalChance);
            var critMultiplier = Math.Pow(CriticalMultiplier, critPower);

            return Damage * critMultiplier;
        }

        public void TakeDamage(double damage)
        {
            Health -= Math.Max(damage - Defense, 0);
        }
    }
}
