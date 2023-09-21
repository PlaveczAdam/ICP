using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ShowSkillDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public double ResourceCost { get; set; }
        public int Cooldown { get; set; }
    }
}
