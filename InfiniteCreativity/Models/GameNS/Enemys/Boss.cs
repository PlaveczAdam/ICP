using InfiniteCreativity.Models.CoreNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Boss : Enemy
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }
        [NotMapped]
        public override double MaxHealth => base.MaxHealth * 2;
    }
}
