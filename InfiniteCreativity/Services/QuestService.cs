﻿using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Services.QuestGeneratorNS;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services
{
    public class QuestService : IQuestService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;
        private QuestGenerator _questGenerator;
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;

        public QuestService(
            IMapper mapper,
            IPlayerService playerService,
            QuestGenerator questGenerator
,
            InfiniteCreativityContext context,
            ICharacterService characterService)
        {
            _mapper = mapper;
            _playerService = playerService;
            _questGenerator = questGenerator;
            _context = context;
            _characterService = characterService;
        }

        public async Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await _characterService.GetCharacterById(characterId, currentPlayer);

            var quests = _context.Quest.Include((x)=>x.Rewards).Where(x => x.Character.Id == character.Id);

            return _mapper.Map<IEnumerable<ShowQuestDTO>>(quests);
        }

        public async Task<ShowQuestDTO> MakeQuestProgress(int questId, int amount)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var q =
                await _context.Quest
                .Include(x => x.Rewards)
                .FirstOrDefaultAsync(x => x.Id == questId)!
                ?? throw new UnauthorizedOperationException();
            var character = _characterService.GetCharacterById(q.Character.Id, currentPlayer);
            if (q.IsDone)
            {
                throw new UnauthorizedOperationException();
            }

            q.Progression = Math.Clamp(q.Progression + amount, 0, 100);

            if (q.Progression == 100)
            {
                q.Rewards.ToList().ForEach(x => x.Player = currentPlayer);
                currentPlayer.Purse += q.CashReward;
                q.IsDone = true;

                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ShowQuestDTO>(q);
        }

        public async Task<ShowQuestDTO> CreateQuest(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await  _characterService.GetCharacterById(characterId, currentPlayer);
            var quest = _questGenerator.Generate();
            quest.Character = character;

            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShowQuestDTO>(quest);
        }

        
    }
}
