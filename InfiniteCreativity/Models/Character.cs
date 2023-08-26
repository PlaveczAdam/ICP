using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.Weapons;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Level { get; set; }

        public double BaseHealth { get; set; }
        [NotMapped]
        public double Health => BaseHealth*Level;

        public IEnumerable<Quest>? Quests { get; set; }

        public Head? Head { get; set; }
        public Shoulder? Shoulder { get; set; }
        public Chest? Chest { get; set; }
        public Hand? Hand { get; set; }
        public Leg? Leg { get; set; }
        public Boot? Boot { get; set; }
        public Weapon? Weapon { get; set; }

        public Player Player { get; set; }
    }
}
