﻿using InfiniteCreativity.Models.CoreNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.GameNS.Enemys
{
    public class Boss : Enemy
    {
        public Boss(BattleParticipant? battleParticipant) : base(battleParticipant)
        {
        }

        public string Name { get; set; }
        [NotMapped]
        public override double MaxHealth => base.MaxHealth * 2;
    }
}
