using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowCharacterSkillSlotDTO
    {
        public Guid Id { get; set; }
        public int SlotNumber { get; set; }
        public ShowSkillHolderDTO? SkillHolder { get; set; }
        public int CurrentCooldown { get; set; }
    }
}
