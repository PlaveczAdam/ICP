﻿// <auto-generated />
using System;
using InfiniteCreativity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    [DbContext(typeof(InfiniteCreativityContext))]
    partial class InfiniteCreativityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataObjects.EntityBaseDataObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("HexTileDataObjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HexTileDataObjectId")
                        .IsUnique();

                    b.ToTable("EntityBase");
                });

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Columns")
                        .HasColumnType("integer");

                    b.Property<Guid>("GConnectionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GConnectionId")
                        .IsUnique();

                    b.ToTable("Map");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.BattleParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BattleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<int>("CurrentActionGauge")
                        .HasColumnType("integer");

                    b.Property<double>("CurrentSpeed")
                        .HasColumnType("double precision");

                    b.Property<Guid?>("EnemyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("EnemyId");

                    b.ToTable("BattleParticipants");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BootId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ChestId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Col")
                        .HasColumnType("integer");

                    b.Property<double>("CurrentAbilityResource")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentHealth")
                        .HasColumnType("double precision");

                    b.Property<int>("CurrentMovement")
                        .HasColumnType("integer");

                    b.Property<Guid?>("HandId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("HeadId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsInCombat")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LegId")
                        .HasColumnType("uuid");

                    b.Property<double>("Level")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Profession")
                        .HasColumnType("integer");

                    b.Property<int>("Race")
                        .HasColumnType("integer");

                    b.Property<int?>("Row")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ShoulderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WeaponId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BootId");

                    b.HasIndex("ChestId");

                    b.HasIndex("HandId");

                    b.HasIndex("HeadId");

                    b.HasIndex("LegId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ShoulderId");

                    b.HasIndex("WeaponId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.CharacterSkillSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SkillHolderId")
                        .HasColumnType("uuid");

                    b.Property<int>("SlotNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("SkillHolderId");

                    b.ToTable("CharacterSkillSlot");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.FeConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Connected")
                        .HasColumnType("boolean");

                    b.Property<string>("ConnectionID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("FeConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ItemType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("QuestId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rarity")
                        .HasColumnType("integer");

                    b.Property<int?>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("QuestId");

                    b.ToTable("Item");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Listing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ListingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SellerId");

                    b.ToTable("Listing");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CharacterSlot")
                        .HasColumnType("integer");

                    b.Property<double>("Money")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuestSlot")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Quest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CashReward")
                        .HasColumnType("double precision");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<double>("LevelReward")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Progression")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Quest");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AbilityGaugeCost")
                        .HasColumnType("integer");

                    b.Property<int>("Cooldown")
                        .HasColumnType("integer");

                    b.Property<double>("Damage")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ResourceCost")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Skill");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"),
                            AbilityGaugeCost = 2,
                            Cooldown = 0,
                            Damage = 2.0,
                            Description = "nincs",
                            Name = "First",
                            ResourceCost = 1.0
                        },
                        new
                        {
                            Id = new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09"),
                            AbilityGaugeCost = 1,
                            Cooldown = 2,
                            Damage = 2.0,
                            Description = "good for health",
                            Name = "GenericHealing",
                            ResourceCost = 2.0
                        });
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Battle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("HasStarted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("NextInTurnId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NextInTurnId");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Enemy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnemyType")
                        .HasColumnType("integer");

                    b.Property<Guid>("GConnectionId")
                        .HasColumnType("uuid");

                    b.Property<double>("Health")
                        .HasColumnType("double precision");

                    b.Property<double>("Level")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("GConnectionId");

                    b.ToTable("Enemy");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Enemy");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BattleId")
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Turn")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GameCharacter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConnectionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ConnectionId");

                    b.ToTable("GameCharacter");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.HexTileDataObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ColIdx")
                        .HasColumnType("integer");

                    b.Property<Guid?>("EnemyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDiscovered")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MapDataObjectId")
                        .HasColumnType("uuid");

                    b.Property<bool>("ReservedForPath")
                        .HasColumnType("boolean");

                    b.Property<int>("RowIdx")
                        .HasColumnType("integer");

                    b.Property<int>("TileContent")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EnemyId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MapDataObjectId");

                    b.ToTable("HexTiles");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Equippable", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Item");

                    b.Property<int>("EquipCount")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Equippable");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Stackable", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Item");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("StackableType")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Stackable");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Boss", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.GameNS.Enemys.Enemy");

                    b.HasDiscriminator().HasValue("Boss");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Boot", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<int>("Movement")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Boot");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Chest", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Chest");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Hand", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Hand");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Head", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Head");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Leg", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Leg");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.ArmorNs.Shoulder", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Health")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Shoulder");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Weapons.Weapon", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Equippable");

                    b.Property<double>("CritChance")
                        .HasColumnType("double precision");

                    b.Property<double>("CritMultiplier")
                        .HasColumnType("double precision");

                    b.Property<double>("Damage")
                        .HasColumnType("double precision");

                    b.Property<int>("WeaponType")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Weapon");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Materials.Material", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Stackable");

                    b.HasDiscriminator().HasValue("Material");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.SkillHolder", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Stackable");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.HasIndex("SkillId");

                    b.HasDiscriminator().HasValue("SkillHolder");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Weapons.Melee", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Weapons.Weapon");

                    b.HasDiscriminator().HasValue("Melee");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Weapons.Ranged", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.CoreNS.Weapons.Weapon");

                    b.Property<double>("Reload")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Ranged");
                });

            modelBuilder.Entity("DataObjects.EntityBaseDataObject", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.HexTileDataObject", "HexTileDataObject")
                        .WithOne("DetailEntity")
                        .HasForeignKey("DataObjects.EntityBaseDataObject", "HexTileDataObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HexTileDataObject");
                });

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.GConnection", "GConnection")
                        .WithOne("Map")
                        .HasForeignKey("Entities.MapDataObject", "GConnectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.BattleParticipant", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.Battle", null)
                        .WithMany("Participants")
                        .HasForeignKey("BattleId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("InfiniteCreativity.Models.GameNS.Enemys.Enemy", "Enemy")
                        .WithMany()
                        .HasForeignKey("EnemyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Character");

                    b.Navigation("Enemy");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Character", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Boot", "Boot")
                        .WithMany()
                        .HasForeignKey("BootId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Chest", "Chest")
                        .WithMany()
                        .HasForeignKey("ChestId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Hand", "Hand")
                        .WithMany()
                        .HasForeignKey("HandId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Head", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Leg", "Leg")
                        .WithMany()
                        .HasForeignKey("LegId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.CoreNS.ArmorNs.Shoulder", "Shoulder")
                        .WithMany()
                        .HasForeignKey("ShoulderId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Weapons.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId");

                    b.Navigation("Boot");

                    b.Navigation("Chest");

                    b.Navigation("Hand");

                    b.Navigation("Head");

                    b.Navigation("Leg");

                    b.Navigation("Player");

                    b.Navigation("Shoulder");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.CharacterSkillSlot", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Character", "Character")
                        .WithMany("SkillSlots")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.CoreNS.SkillHolder", "SkillHolder")
                        .WithMany()
                        .HasForeignKey("SkillHolderId");

                    b.Navigation("Character");

                    b.Navigation("SkillHolder");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.FeConnection", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", null)
                        .WithMany("FeConnections")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Item", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Player")
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Quest", null)
                        .WithMany("Rewards")
                        .HasForeignKey("QuestId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Listing", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Seller")
                        .WithMany("Listing")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Message", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Recipient")
                        .WithMany("RecievedMessages")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Quest", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Character", "Character")
                        .WithMany("Quests")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Battle", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.BattleParticipant", "NextInTurn")
                        .WithMany()
                        .HasForeignKey("NextInTurnId");

                    b.Navigation("NextInTurn");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Enemy", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.GConnection", "GConnection")
                        .WithMany("Enemies")
                        .HasForeignKey("GConnectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GConnection", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.Battle", "Battle")
                        .WithMany()
                        .HasForeignKey("BattleId");

                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", "Player")
                        .WithMany("GConnections")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GameCharacter", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.GameNS.GConnection", "Connection")
                        .WithMany("Characters")
                        .HasForeignKey("ConnectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Connection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.HexTileDataObject", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.Enemys.Enemy", "Enemy")
                        .WithOne("Tile")
                        .HasForeignKey("InfiniteCreativity.Models.GameNS.HexTileDataObject", "EnemyId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Entities.MapDataObject", "MapDataObject")
                        .WithMany("HexTiles")
                        .HasForeignKey("MapDataObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enemy");

                    b.Navigation("MapDataObject");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.SkillHolder", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.Navigation("HexTiles");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Character", b =>
                {
                    b.Navigation("Quests");

                    b.Navigation("SkillSlots");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Player", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("FeConnections");

                    b.Navigation("GConnections");

                    b.Navigation("Inventory");

                    b.Navigation("Listing");

                    b.Navigation("RecievedMessages");

                    b.Navigation("SentMessages");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Quest", b =>
                {
                    b.Navigation("Rewards");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Battle", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Enemy", b =>
                {
                    b.Navigation("Tile");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GConnection", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Enemies");

                    b.Navigation("Map")
                        .IsRequired();
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.HexTileDataObject", b =>
                {
                    b.Navigation("DetailEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
