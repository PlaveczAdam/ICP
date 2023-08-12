using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Services.ItemGeneratorNS;

namespace InfiniteCreativity.Services.QuestGeneratorNS
{
    public class QuestGenerator
    {
        private Random _random = new Random();
        private ItemGenerator _itemGenerator;

        public QuestGenerator(ItemGenerator itemGenerator)
        {
            _itemGenerator = itemGenerator;
        }

        private static List<QuestDescription> _questDescriptions = new List<QuestDescription>()
        {
            new QuestDescription { Name = "Something", Description = "even more something" },
            new QuestDescription { Name = "Something1", Description = "even more something1" },
            new QuestDescription { Name = "Something2", Description = "even more something2" },
        };

        public Quest Generate()
        {
            var quest = new Quest();
            var questDesc = _random.Next(_questDescriptions);
            quest.Name = questDesc.Name;
            quest.Description = questDesc.Description;
            quest.Rewards = new List<Item> { _itemGenerator.Generate() };
            quest.CashReward = Math.Round(_random.NextDouble(10,50));
            quest.LevelReward = _random.NextDouble(0.1, 0.3);
            quest.Duration = _random.NextTimeDuration(1, 5);

            return quest;
        }
    }
}
