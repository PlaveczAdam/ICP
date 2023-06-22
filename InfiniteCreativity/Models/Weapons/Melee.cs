using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.Weapons
{
    public class Melee : Weapon
    {
        [NotMapped]
        public int Test => 0;
    }
}
