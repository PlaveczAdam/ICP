﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS.Weapons
{
    public class Melee : Weapon
    {
        [NotMapped]
        public int Test => 0;
    }
}
