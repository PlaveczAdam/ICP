using InfiniteCreativity.Models.CoreNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Boss : Enemy
    {
        private string _name;

        public override string Name => BossName;
        public string BossName { get; set; }
        [NotMapped]
        public override double MaxHealth => base.MaxHealth * 2;
    }
}
