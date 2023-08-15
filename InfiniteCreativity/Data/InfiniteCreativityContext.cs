using Microsoft.EntityFrameworkCore;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.Materials;

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
        public DbSet<Equippable> Equippable { get; set; }
        public DbSet<Quest> Quest { get; set; }
        public DbSet<Head> Head { get; set; }
        public DbSet<Shoulder> Shoulder { get; set; }
        public DbSet<Chest> Chest { get; set; }
        public DbSet<Hand> Hand { get; set; }
        public DbSet<Leg> Leg { get; set; }
        public DbSet<Boot> Boot { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Stackable> Stackable { get; set; }
        public DbSet<FeConnection> FeConnection { get; set; }
        public DbSet<GConnection> GConnection { get; set; }

    }
}
