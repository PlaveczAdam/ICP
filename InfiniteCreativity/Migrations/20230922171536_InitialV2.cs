using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class InitialV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Money = table.Column<double>(type: "double precision", nullable: false),
                    CharacterSlot = table.Column<int>(type: "integer", nullable: false),
                    QuestSlot = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Damage = table.Column<double>(type: "double precision", nullable: false),
                    ResourceCost = table.Column<double>(type: "double precision", nullable: false),
                    Cooldown = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeConnection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConnectionID = table.Column<string>(type: "text", nullable: false),
                    UserAgent = table.Column<string>(type: "text", nullable: false),
                    Connected = table.Column<bool>(type: "boolean", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeConnection_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GConnection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConnectionID = table.Column<string>(type: "text", nullable: false),
                    Turn = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GConnection_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageBody = table.Column<string>(type: "text", nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Player_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Player_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enemy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<double>(type: "double precision", nullable: false),
                    EnemyType = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<double>(type: "double precision", nullable: false),
                    GConnectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enemy_GConnection_GConnectionId",
                        column: x => x.GConnectionId,
                        principalTable: "GConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rows = table.Column<int>(type: "integer", nullable: false),
                    Columns = table.Column<int>(type: "integer", nullable: false),
                    GConnectionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Map_GConnection_GConnectionId",
                        column: x => x.GConnectionId,
                        principalTable: "GConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HexTiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MapDataObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowIdx = table.Column<int>(type: "integer", nullable: false),
                    ColIdx = table.Column<int>(type: "integer", nullable: false),
                    TileContent = table.Column<int>(type: "integer", nullable: false),
                    IsDiscovered = table.Column<bool>(type: "boolean", nullable: false),
                    ReservedForPath = table.Column<bool>(type: "boolean", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HexTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HexTiles_Enemy_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HexTiles_Map_MapDataObjectId",
                        column: x => x.MapDataObjectId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HexTileDataObjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityBase_HexTiles_HexTileDataObjectId",
                        column: x => x.HexTileDataObjectId,
                        principalTable: "HexTiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: true),
                    EnemyId = table.Column<int>(type: "integer", nullable: true),
                    CurrentSpeed = table.Column<double>(type: "double precision", nullable: false),
                    BattleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleParticipants_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BattleParticipants_Enemy_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<double>(type: "double precision", nullable: false),
                    Race = table.Column<int>(type: "integer", nullable: false),
                    Profession = table.Column<int>(type: "integer", nullable: false),
                    CurrentMovement = table.Column<int>(type: "integer", nullable: false),
                    CurrentHealth = table.Column<double>(type: "double precision", nullable: false),
                    IsInCombat = table.Column<bool>(type: "boolean", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    ShoulderId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChestId = table.Column<Guid>(type: "uuid", nullable: true),
                    HandId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegId = table.Column<Guid>(type: "uuid", nullable: true),
                    BootId = table.Column<Guid>(type: "uuid", nullable: true),
                    WeaponId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: true),
                    Col = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameCharacter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ConnectionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCharacter_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCharacter_GConnection_ConnectionId",
                        column: x => x.ConnectionId,
                        principalTable: "GConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Progression = table.Column<double>(type: "double precision", nullable: false),
                    CashReward = table.Column<double>(type: "double precision", nullable: false),
                    LevelReward = table.Column<double>(type: "double precision", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quest_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: true),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    QuestId = table.Column<Guid>(type: "uuid", nullable: true),
                    EquipCount = table.Column<int>(type: "integer", nullable: true),
                    Movement = table.Column<int>(type: "integer", nullable: true),
                    ArmorType = table.Column<int>(type: "integer", nullable: true),
                    ArmorValue = table.Column<double>(type: "double precision", nullable: true),
                    Health = table.Column<double>(type: "double precision", nullable: true),
                    WeaponType = table.Column<int>(type: "integer", nullable: true),
                    Damage = table.Column<double>(type: "double precision", nullable: true),
                    CritChance = table.Column<double>(type: "double precision", nullable: true),
                    CritMultiplier = table.Column<double>(type: "double precision", nullable: true),
                    Reload = table.Column<double>(type: "double precision", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    StackableType = table.Column<int>(type: "integer", nullable: true),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkillSlot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillHolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSkillSlot_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                        column: x => x.SkillHolderId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ListingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listing_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listing_Player_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Cooldown", "Damage", "Description", "Name", "ResourceCost" },
                values: new object[] { new Guid("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"), 0, 2.0, "nincs", "First", 1.0 });

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_BattleId",
                table: "BattleParticipants",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_BootId",
                table: "Character",
                column: "BootId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_ChestId",
                table: "Character",
                column: "ChestId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_HandId",
                table: "Character",
                column: "HandId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_HeadId",
                table: "Character",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_LegId",
                table: "Character",
                column: "LegId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlayerId",
                table: "Character",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_ShoulderId",
                table: "Character",
                column: "ShoulderId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_WeaponId",
                table: "Character",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillSlot_CharacterId",
                table: "CharacterSkillSlot",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillSlot_SkillHolderId",
                table: "CharacterSkillSlot",
                column: "SkillHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Enemy_GConnectionId",
                table: "Enemy",
                column: "GConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityBase_HexTileDataObjectId",
                table: "EntityBase",
                column: "HexTileDataObjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeConnection_PlayerId",
                table: "FeConnection",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCharacter_CharacterId",
                table: "GameCharacter",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCharacter_ConnectionId",
                table: "GameCharacter",
                column: "ConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GConnection_PlayerId",
                table: "GConnection",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_EnemyId",
                table: "HexTiles",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_Id",
                table: "HexTiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_MapDataObjectId",
                table: "HexTiles",
                column: "MapDataObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerId",
                table: "Item",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_QuestId",
                table: "Item",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SkillId",
                table: "Item",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_ItemId",
                table: "Listing",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_SellerId",
                table: "Listing",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionId",
                table: "Map",
                column: "GConnectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecipientId",
                table: "Message",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_CharacterId",
                table: "Quest",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Character_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_BootId",
                table: "Character",
                column: "BootId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_ChestId",
                table: "Character",
                column: "ChestId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_HandId",
                table: "Character",
                column: "HandId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_HeadId",
                table: "Character",
                column: "HeadId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_LegId",
                table: "Character",
                column: "LegId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_ShoulderId",
                table: "Character",
                column: "ShoulderId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_WeaponId",
                table: "Character",
                column: "WeaponId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest");

            migrationBuilder.DropTable(
                name: "BattleParticipants");

            migrationBuilder.DropTable(
                name: "CharacterSkillSlot");

            migrationBuilder.DropTable(
                name: "EntityBase");

            migrationBuilder.DropTable(
                name: "FeConnection");

            migrationBuilder.DropTable(
                name: "GameCharacter");

            migrationBuilder.DropTable(
                name: "Listing");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "HexTiles");

            migrationBuilder.DropTable(
                name: "Enemy");

            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropTable(
                name: "GConnection");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}
