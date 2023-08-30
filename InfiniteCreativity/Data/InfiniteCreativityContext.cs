using Microsoft.EntityFrameworkCore;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.GameNS.Enemys;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.GameNS;
using Entities;
using DataObjects;
using System.Reflection.Metadata;

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
        public DbSet<Enemy> Enemy { get; set; }
        public DbSet<Boss> Boss { get; set; }
        public DbSet<MapDataObject> Map { get; set; }
        public DbSet<HexTileDataObject> HexTiles { get; set; }
        public DbSet<EntityBaseDataObject> EntityBase { get; set; }
        public DbSet<GameCharacter> GameCharacter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapDataObject>()
                .HasOne(e => e.GConnection)
                .WithOne(e => e.Map)
                .HasForeignKey<MapDataObject>(e => e.GConnectionId)
                .IsRequired();

            modelBuilder.Entity<HexTileDataObject>()
                .HasOne(e => e.DetailEntity)
                .WithOne(e => e.HexTileDataObject)
                .HasForeignKey<HexTileDataObject>(e => e.DetailEntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
