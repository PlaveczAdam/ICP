using AutoMapper;
using DTOs;
using DTOs.Enums.CoreNS;
using DTOs.Enums.GameNS;
using DTOs.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.Enums.CoreNS;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Skill
    {
        private Random _rnd = new Random();
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public double ResourceCost { get; set; }
        public int AbilityGaugeCost { get; set; }
        public int Cooldown { get; set; }
        public TargetType TargetType { get; set; }
        public ICollection<BuffBlueprint> Buffs { get; set; } = new List<BuffBlueprint>();
        public ICollection<ConditionBlueprint> Conditions { get; set; } = new List<ConditionBlueprint>();

        public static Dictionary<StackableType, Skill> SkillSeed = new Dictionary<StackableType, Skill>() {
            {
                StackableType.FirstSkill,
                new Skill {
                    Id = Guid.Parse("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"),
                    Name = "First",
                    Description = "nincs",
                    Cooldown = 0,
                    ResourceCost = 1,
                    AbilityGaugeCost = 2,
                    Damage = 2,
                    TargetType = TargetType.Enemy
                }
            },
            {
                StackableType.GenericDebuff,
                new Skill {
                    Id = Guid.Parse("0DD69A53-D1FD-4D80-8ADD-AF15AC0666A6"),
                    Name = "Weakness",
                    Description = "nincs",
                    Cooldown = 0,
                    ResourceCost = 1,
                    AbilityGaugeCost = 2,
                    Damage = 0,
                    TargetType = TargetType.Enemy
                }
            },
            {
                StackableType.HealSkill,
                new Skill {
                    Id = Guid.Parse("be29078b-1e09-4b15-8802-77a8e3c8fd09"),
                    Name = "GenericHealing",
                    Description = "good for health",
                    Cooldown = 2,
                    ResourceCost = 2,
                    AbilityGaugeCost = 1,
                    Damage = 2,
                    TargetType = TargetType.Ally,
                }
            },
            {
                StackableType.ContinousBuff,
                new Skill {
                    Id = Guid.Parse("1CEFB293-B21C-415C-A2F9-A8B74104624E"),
                    Name = "Generic Continous Buff",
                    Description = "buff",
                    Cooldown = 2,
                    ResourceCost = 2,
                    AbilityGaugeCost = 1,
                    Damage = 0,
                    TargetType = TargetType.Ally,
                }
            },
        };

        public static Dictionary<StackableType, SkillHolder> SkillHolder = new Dictionary<StackableType, SkillHolder>() {
            {
                StackableType.FirstSkill,
                new SkillHolder {
                    Name = "First",
                    Description = "nincs",
                    ImageName = ImageName.Stone,
                    ItemType = ItemType.Skill,
                    StackableType = StackableType.FirstSkill,
                    Value = 1,
                    Rarity = RarityType.Common,
                    SkillId = SkillSeed[StackableType.FirstSkill].Id,
                }
            },
            {
                StackableType.GenericDebuff,
                new SkillHolder {
                    Name = "Debuff",
                    Description = "nincs",
                    ImageName = ImageName.Stone,
                    ItemType = ItemType.Skill,
                    StackableType = StackableType.GenericDebuff,
                    Value = 1,
                    Rarity = RarityType.Common,
                    SkillId = SkillSeed[StackableType.GenericDebuff].Id,
                }
            },
            {
                StackableType.HealSkill,
                new SkillHolder {
                    Name = "GenericHealing",
                    Description = "good for health",
                    ImageName = ImageName.TheRock,
                    ItemType = ItemType.Skill,
                    StackableType = StackableType.HealSkill,
                    Value = 1,
                    Rarity = RarityType.Common,
                    SkillId = SkillSeed[StackableType.HealSkill].Id,
                }
            },
            {
                StackableType.ContinousBuff,
                new SkillHolder {
                    Name = "Generic Continous Buff",
                    Description = "buff",
                    ImageName = ImageName.TheRock,
                    ItemType = ItemType.Skill,
                    StackableType = StackableType.ContinousBuff,
                    Value = 1,
                    Rarity = RarityType.Common,
                    SkillId = SkillSeed[StackableType.ContinousBuff].Id,
                }
            },
        };

        public static Dictionary<StackableType, List<ConditionBlueprint>> ConditionBlueprintSeed = new Dictionary<StackableType, List<ConditionBlueprint>>() {
            {
                StackableType.FirstSkill,
                new List<ConditionBlueprint> () {
                    new ConditionBlueprint
                    {
                        ID = Guid.Parse("C0AEFCAB-0958-469F-A331-EA1B0967B557"),
                        ConditionType = ConditionType.Bleed,
                        Duration = 10,
                        SkillId = SkillSeed[StackableType.FirstSkill].Id
                    },
                }
            },
            {
                StackableType.GenericDebuff,
                new List<ConditionBlueprint> () {
                    new ConditionBlueprint
                    {
                        ID = Guid.Parse("316EC03B-C2CD-4196-AAEF-E3FA0C203D6D"),
                        ConditionType = ConditionType.Weakness,
                        Duration = 10,
                        SkillId = SkillSeed[StackableType.GenericDebuff].Id
                    },
                }
            },
        };

        public static Dictionary<StackableType, List<BuffBlueprint>> BuffBlueprintSeed = new Dictionary<StackableType, List<BuffBlueprint>>() {
            {
                StackableType.HealSkill,
                new List<BuffBlueprint> () {
                    new BuffBlueprint
                    {
                        ID = Guid.Parse("96660F57-F437-4E15-A469-7F596C6CCCCC"),
                        BuffType = BuffType.Rejuvenation,
                        Duration = 10,
                        SkillId = SkillSeed[StackableType.HealSkill].Id
                    },
                }
            },

            {
                StackableType.ContinousBuff,
                new List<BuffBlueprint> () {
                    new BuffBlueprint
                    {
                        ID = Guid.Parse("B4B2E548-24F8-4A86-8308-B634284FB0E8"),
                        BuffType = BuffType.Might,
                        Duration = 10,
                        SkillId = SkillSeed[StackableType.ContinousBuff].Id, 
                        Stacks = 99
                    },
                }
            },
        };

        public IEnumerable<ShowBattleEventDTO> Activate(BattleParticipant enemy, BattleParticipant caster, IMapper mapper )
        {
            var damage = Damage * caster.Character!.AbilityDamage;
            var crit = _rnd.NextCrit(caster.Character.CriticalChance);
            var modifier = caster.CalculateStatModifications();
            damage *= Math.Pow(caster.Character.CriticalMultiplier,  crit) * modifier.DamageMultiplier;

            enemy.Enemy!.TakeDamage(damage);
            caster.Character.CurrentAbilityResource -= ResourceCost;
            caster.CurrentActionGauge -= AbilityGaugeCost;

            return new List<ShowBattleEventDTO>()
            {
                new ShowBattleEventCharacterAttackDTO()
                {
                    SourceParticipantId = caster.Id,
                    TargetParticipantId = enemy.Id,
                    NewTargetHp = enemy.Enemy.Health,
                    NewAbilityGauge = caster.CurrentActionGauge,
                    NewResource = caster.Character.CurrentAbilityResource,
                    Skill = mapper.Map<ShowSkillDTO>(this),
                }
            };
        }

        public IEnumerable<ShowBattleEventDTO> ActivateHeal(BattleParticipant character, BattleParticipant caster, IMapper mapper)
        {
            var heal = Damage * caster.Character!.AbilityDamage;
            var crit = _rnd.NextCrit(caster.Character.CriticalChance);
            heal *= Math.Pow(caster.Character.CriticalMultiplier, crit);

            character.Character!.TakeHealing(heal);
            caster.Character.CurrentAbilityResource -= ResourceCost;
            caster.CurrentActionGauge -= AbilityGaugeCost;

            return new List<ShowBattleEventDTO>()
            {
                new ShowBattleEventCharacterHealDTO()
                {
                    SourceParticipantId = caster.Id,
                    TargetParticipantId = character.Id,
                    NewTargetHp = character.Character.CurrentHealth,
                    NewAbilityGauge = caster.CurrentActionGauge,
                    NewResource = caster.Character.CurrentAbilityResource,
                    Skill = mapper.Map<ShowSkillDTO>(this),
                }
            };
        }

        internal IEnumerable<ShowBattleEventDTO> ActivateBuffOnly(BattleParticipant character, BattleParticipant caster, IMapper mapper)
        {
            caster.Character.CurrentAbilityResource -= ResourceCost;
            caster.CurrentActionGauge -= AbilityGaugeCost;

            return new List<ShowBattleEventDTO>()
            {
                new ShowBattleEventCharacterBuffOnlyDTO()
                {
                    SourceParticipantId = caster.Id,
                    TargetParticipantId = character.Id,
                    NewAbilityGauge = caster.CurrentActionGauge,
                    NewResource = caster.Character.CurrentAbilityResource,
                    Skill = mapper.Map<ShowSkillDTO>(this),
                }
            };
        }
    }
}
