using InfiniteCreativity.DTO;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [Authorize]
    [ApiController, Route("/api/quest")]
    public class QuestController
    {
        private IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpGet, Route("{characterId}")]
        public Task<IEnumerable<ShowQuestDTO>> GetQuests(Guid characterId)
        {
            return _questService.GetQuestByCharacterId(characterId);
        }

        [HttpPost, Route("{characterId}")]
        public Task<ShowQuestDTO> CreateQuest(Guid characterId)
        {
            return _questService.CreateQuest(characterId);
        }

        [HttpPut, Route("{questId}/{amount}")]
        public async Task<ShowQuestDTO> StatusUpdate(Guid questId, int amount)
        {
            return await _questService.MakeQuestProgress(questId, amount);
        }
    }
}
