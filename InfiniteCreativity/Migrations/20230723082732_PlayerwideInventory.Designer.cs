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
    [Migration("20230723082732_PlayerwideInventory")]
    partial class PlayerwideInventory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("Purse")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Character");
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

            modelBuilder.Entity("InfiniteCreativity.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Progression")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Quest");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Weapons.Melee", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Item");

                    b.Property<double>("AttackSpeed")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("CritChance")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("CritMultiplier")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Damage")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Range")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<int>("WeaponType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Melee");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Weapons.Ranged", b =>
                {
                    b.HasBaseType("InfiniteCreativity.Models.Item");

                    b.Property<double>("AttackSpeed")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("CritChance")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("CritMultiplier")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Damage")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Range")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("double precision");

                    b.Property<double>("Reload")
                        .HasColumnType("double precision");

                    b.Property<int>("WeaponType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Ranged");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Character", b =>
                {
                    b.HasOne("InfiniteCreativity.Models.Player", null)
                        .WithMany("Characters")
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

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("InfiniteCreativity.Models.Quest", b =>
                {
                    b.Navigation("Rewards");
                });
#pragma warning restore 612, 618
        }
    }
}
