using DTOs.Enums.GameNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowConditionDTO
    {
        public Guid ID { get; set; }   
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public ConditionType ConditionType { get; set; }
        public double ConditionDamageMultiplier { get; set; }
        public bool StacksDuration { get; set; }
    }
}
