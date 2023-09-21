using InfiniteCreativity.Models.Enums.CoreNS;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public double ResourceCost { get; set; }
        public int Cooldown { get; set; }

        public static Dictionary<StackableType, Skill> SkillSeed = new Dictionary<StackableType, Skill>() { 
            { 
                StackableType.FirstSkill, 
                new Skill { 
                    Id = Guid.Parse("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"),
                    Name = "First",
                    Description = "nincs",
                    Cooldown = 0,
                    ResourceCost = 1,
                    Damage = 2,
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
        };
    }
}
