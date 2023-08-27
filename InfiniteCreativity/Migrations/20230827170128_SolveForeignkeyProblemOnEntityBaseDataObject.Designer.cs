﻿// <auto-generated />
using System;
using InfiniteCreativity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    [DbContext(typeof(InfiniteCreativityContext))]
    [Migration("20230827170128_SolveForeignkeyProblemOnEntityBaseDataObject")]
    partial class SolveForeignkeyProblemOnEntityBaseDataObject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataObjects.EntityBaseDataObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("EntityBase");
                });

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Columns")
                        .HasColumnType("integer");

                    b.Property<string>("GConnectionConnectionID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GConnectionConnectionID");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BootId")
                        .HasColumnType("integer");

                    b.Property<int?>("ChestId")
                        .HasColumnType("integer");

                    b.Property<int?>("Col")
                        .HasColumnType("integer");

                    b.Property<double>("CurrentHealth")
                        .HasColumnType("double precision");

                    b.Property<int?>("HandId")
                        .HasColumnType("integer");

                    b.Property<int?>("HeadId")
                        .HasColumnType("integer");

                    b.Property<int?>("LegId")
                        .HasColumnType("integer");

                    b.Property<double>("Level")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("Profession")
                        .HasColumnType("integer");

                    b.Property<int>("Race")
                        .HasColumnType("integer");

                    b.Property<int?>("Row")
                        .HasColumnType("integer");

                    b.Property<int?>("ShoulderId")
                        .HasColumnType("integer");

                    b.Property<int?>("WeaponId")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.FeConnection", b =>
                {
                    b.Property<string>("ConnectionID")
                        .HasColumnType("text");

                    b.Property<bool>("Connected")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ConnectionID");

                    b.HasIndex("PlayerId");

                    b.ToTable("FeConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int?>("QuestId")
                        .HasColumnType("integer");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ListingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("SellerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SellerId");

                    b.ToTable("Listing");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CashReward")
                        .HasColumnType("double precision");

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Enemy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnemyType")
                        .HasColumnType("integer");

                    b.Property<string>("GConnectionConnectionID")
                        .HasColumnType("text");

                    b.Property<double>("Health")
                        .HasColumnType("double precision");

                    b.Property<double>("Level")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("GConnectionConnectionID");

                    b.ToTable("Enemy");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Enemy");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GConnection", b =>
                {
                    b.Property<string>("ConnectionID")
                        .HasColumnType("text");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("Turn")
                        .HasColumnType("integer");

                    b.HasKey("ConnectionID");

                    b.HasIndex("PlayerId");

                    b.ToTable("GConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.HexTileDataObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ColIdx")
                        .HasColumnType("integer");

                    b.Property<int?>("DetailEntityId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDiscovered")
                        .HasColumnType("boolean");

                    b.Property<int>("MapDataObjectId")
                        .HasColumnType("integer");

                    b.Property<bool>("ReservedForPath")
                        .HasColumnType("boolean");

                    b.Property<int>("RowIdx")
                        .HasColumnType("integer");

                    b.Property<int>("TileContent")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DetailEntityId");

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

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

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

                    b.Property<double>("AttackSpeed")
                        .HasColumnType("double precision");

                    b.Property<double>("CritChance")
                        .HasColumnType("double precision");

                    b.Property<double>("CritMultiplier")
                        .HasColumnType("double precision");

                    b.Property<double>("Damage")
                        .HasColumnType("double precision");

                    b.Property<double>("Range")
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

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.GConnection", "GConnection")
                        .WithMany()
                        .HasForeignKey("GConnectionConnectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GConnection");
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

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.Enemys.Enemy", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.GameNS.GConnection", "GConnection")
                        .WithMany()
                        .HasForeignKey("GConnectionConnectionID");

                    b.Navigation("GConnection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.GConnection", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.CoreNS.Player", null)
                        .WithMany("GConnections")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.GameNS.HexTileDataObject", b =>
                {
                    b.HasOne("DataObjects.EntityBaseDataObject", "DetailEntity")
                        .WithMany()
                        .HasForeignKey("DetailEntityId");

                    b.HasOne("Entities.MapDataObject", "MapDataObject")
                        .WithMany("HexTiles")
                        .HasForeignKey("MapDataObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetailEntity");

                    b.Navigation("MapDataObject");
                });

            modelBuilder.Entity("Entities.MapDataObject", b =>
                {
                    b.Navigation("HexTiles");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.CoreNS.Character", b =>
                {
                    b.Navigation("Quests");
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
#pragma warning restore 612, 618
        }
    }
}
