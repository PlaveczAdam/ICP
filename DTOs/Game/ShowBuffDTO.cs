using DTOs.Enums.CoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowBuffDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BuffType BuffType { get; set; }
        public int Duration { get; set; }
        public bool StacksDuration { get; set; }
    }
}
