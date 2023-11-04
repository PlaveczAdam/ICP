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
        public DbSet<Skill> Skill { get; set; }
        public DbSet<SkillHolder> SkillHolder { get; set; }
        public DbSet<CharacterSkillSlot> CharacterSkillSlot { get; set; }
        public DbSet<BattleParticipant> BattleParticipants { get; set; }
        public DbSet<Battle> Battle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapDataObject>()
                .HasOne(e => e.GConnection)
                .WithOne(e => e.Map)
                .HasForeignKey<MapDataObject>(e => e.GConnectionId)
                .IsRequired();

            modelBuilder.Entity<EntityBaseDataObject>()
                .HasOne(e => e.HexTileDataObject)
                .WithOne(e => e.DetailEntity)
                .HasForeignKey<EntityBaseDataObject>(e => e.HexTileDataObjectId);

            modelBuilder.Entity<HexTileDataObject>()
                .HasIndex(e => e.Id)
                .IsUnique();

            modelBuilder.Entity<GConnection>()
                .HasOne(e => e.Player)
                .WithMany(e => e.GConnections)
                .HasForeignKey(e => e.PlayerId);

            modelBuilder.Entity<Skill>()
                .HasData(InfiniteCreativity.Models.CoreNS.Skill.SkillSeed.Values);

            modelBuilder.Entity<SkillHolder>()
                .HasOne(e => e.Skill)
                .WithMany()
                .HasForeignKey(e => e.SkillId);

            modelBuilder.Entity<CharacterSkillSlot>()
                .HasOne(e => e.SkillHolder)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HexTileDataObject>()
                .HasOne(e => e.Enemy)
                .WithOne(e => e.Tile)
                .HasForeignKey<HexTileDataObject>(e => e.EnemyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BattleParticipant>()
                .HasOne(e => e.Enemy)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BattleParticipant>()
                .HasMany(e => e.Buffs)
                .WithOne(e => e.BattleParticipant)
                .HasForeignKey(e => e.BattleParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Buff>()
                .HasOne(e => e.Caster)
                .WithMany()
                .HasForeignKey(e => e.CasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BattleParticipant>()
                .HasMany(e => e.Conditions)
                .WithOne(e => e.BattleParticipant)
                .HasForeignKey(e => e.BattleParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BattleParticipant>()
                .HasOne(e => e.Enemy)
                .WithOne(e => e.BattleParticipant)
                .HasForeignKey<BattleParticipant>(e => e.EnemyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Condition>()
                .HasOne(e => e.Caster)
                .WithMany()
                .HasForeignKey(e => e.CasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Battle>()
                .HasMany(e => e.Participants)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Battle>()
                .HasOne(e => e.NextInTurn)
                .WithOne()
                .HasForeignKey<Battle>(e => e.NextInTurnId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BattleParticipant>()
                .HasOne(e => e.Minion)
                .WithOne(e => e.BattleParticipant)
                .HasForeignKey<BattleParticipant>(e => e.MinionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BattleParticipant>()
                .HasOne(e => e.Character)
                .WithOne(e => e.BattleParticipant)
                .HasForeignKey<BattleParticipant>(e => e.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Minion>()
                .HasOne(e => e.Caster)
                .WithMany(e => e.OwnedMinions)
                .HasForeignKey(e => e.CasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Boss>()
              .Property(e => e.Name);

            modelBuilder.Entity<Buff>();
            modelBuilder.Entity<Rejuvenation>();
            modelBuilder.Entity<Regeneration>();
            modelBuilder.Entity<Might>();
            modelBuilder.Entity<DefenseUp>();
            modelBuilder.Entity<BuffBlueprint>()
                .HasData(InfiniteCreativity.Models.CoreNS.Skill.BuffBlueprintSeed.SelectMany(x => x.Value));

            modelBuilder.Entity<Condition>();
            modelBuilder.Entity<Bleed>();
            modelBuilder.Entity<Weakness>();
            modelBuilder.Entity<Taunt>();
            modelBuilder.Entity<ConditionBlueprint>()
                .HasData(InfiniteCreativity.Models.CoreNS.Skill.ConditionBlueprintSeed.SelectMany(x => x.Value));

            modelBuilder.Entity<Minion>();
            modelBuilder.Entity<BB>();
            modelBuilder.Entity<MinionBlueprint>()
                .HasData(InfiniteCreativity.Models.CoreNS.Skill.MinionBlueprintSeed.SelectMany(x => x.Value));

        }
    }
}
