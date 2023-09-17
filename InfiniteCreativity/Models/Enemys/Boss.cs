using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.Enemys
{
    public class Boss:Enemy
    {
        public string Name { get; set; }
        [NotMapped]
        public override double MaxHealth => base.MaxHealth * 2; 
    }
}
