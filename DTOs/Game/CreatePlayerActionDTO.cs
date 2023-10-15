using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class CreatePlayerActionDTO
    {

    }

    public class CreatePlayerActionUseSkillOnEnemy : CreatePlayerActionDTO
    { 
        public Guid TargetId { get; set; }
        public Guid? SkillSlotId { get; set; }
    }

    public class CreatePlayerActionUseSkillOnAlly : CreatePlayerActionDTO
    {
        public Guid TargetId { get; set; }
        public Guid? SkillSlotId { get; set; }
    }

    public class CreatePlayerActionFlee :CreatePlayerActionDTO
    { 

    }

    public class CreatePlayerActionSkipTurn : CreatePlayerActionDTO
    {

    }
}
