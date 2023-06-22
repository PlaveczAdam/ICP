using Microsoft.EntityFrameworkCore;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Data
{
    public class InfiniteCreativityContext : DbContext
    {
        public InfiniteCreativityContext(DbContextOptions<InfiniteCreativityContext> options)
            : base(options) { }

        public DbSet<Player> Player { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Melee> MeleeWeapon { get; set; }
        public DbSet<Ranged> RangedWeapon { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Quest> Quest { get; set; }
    }
}
