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

            modelBuilder.Entity("InfiniteCreativity.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("BaseHealth")
                        .HasColumnType("double precision");

                    b.Property<int?>("BootId")
                        .HasColumnType("integer");

                    b.Property<int?>("ChestId")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("InfiniteCreativity.Models.Connection", b =>
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

                    b.ToTable("Connection");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Item", b =>
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

                    b.Property<int>("StackSize")
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

            modelBuilder.Entity("InfiniteCreativity.Models.Listing", b =>
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

            modelBuilder.Entity("InfiniteCreativity.Models.Message", b =>
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

            modelBuilder.Entity("InfiniteCreativity.Models.Player", b =>
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

            modelBuilder.Entity("InfiniteCreativity.Models.Quest", b =>
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

            modelBuilder.Entity("InfiniteCreativity.Models.Equippable", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Item");

                    b.Property<int>("EquipCount")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Equippable");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Boot", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Boot");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Chest", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Chest");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Hand", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Hand");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Head", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Head");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Leg", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Leg");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Armor.Shoulder", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

                    b.Property<int>("ArmorType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.Property<double>("ArmorValue")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Shoulder");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Weapons.Weapon", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Equippable");

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

            modelBuilder.Entity("InfiniteCreativity.Models.Weapons.Melee", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Weapons.Weapon");

                    b.HasDiscriminator().HasValue("Melee");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Weapons.Ranged", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Weapons.Weapon");

                    b.Property<double>("Reload")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Ranged");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Character", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Armor.Boot", "Boot")
                        .WithMany()
                        .HasForeignKey("BootId");

                    b.HasOne("InfiniteCreativity.Models.Armor.Chest", "Chest")
                        .WithMany()
                        .HasForeignKey("ChestId");

                    b.HasOne("InfiniteCreativity.Models.Armor.Hand", "Hand")
                        .WithMany()
                        .HasForeignKey("HandId");

                    b.HasOne("InfiniteCreativity.Models.Armor.Head", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId");

                    b.HasOne("InfiniteCreativity.Models.Armor.Leg", "Leg")
                        .WithMany()
                        .HasForeignKey("LegId");

                    b.HasOne("InfiniteCreativity.Models.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.Armor.Shoulder", "Shoulder")
                        .WithMany()
                        .HasForeignKey("ShoulderId");

                    b.HasOne("InfiniteCreativity.Models.Weapons.Weapon", "Weapon")
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

            modelBuilder.Entity("InfiniteCreativity.Models.Connection", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Player", null)
                        .WithMany("Connections")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Item", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Player", "Player")
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerId");

                    b.HasOne("InfiniteCreativity.Models.Quest", null)
                        .WithMany("Rewards")
                        .HasForeignKey("QuestId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Listing", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.Player", "Seller")
                        .WithMany("Listing")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Message", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Player", "Recipient")
                        .WithMany("RecievedMessages")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniteCreativity.Models.Player", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Quest", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Character", "Character")
                        .WithMany("Quests")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Character", b =>
                {
                    b.Navigation("Quests");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Player", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Connections");

                    b.Navigation("Inventory");

                    b.Navigation("Listing");

                    b.Navigation("RecievedMessages");

                    b.Navigation("SentMessages");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Quest", b =>
                {
                    b.Navigation("Rewards");
                });
#pragma warning restore 612, 618
        }
    }
}
