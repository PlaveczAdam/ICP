﻿using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.GameNS;
using MoreLinq;
using static MoreLinq.Extensions.ForEachExtension;

namespace InfiniteCreativity.Services.MapPatherNS
{
    public class TurnSimulator
    {
        public List<Guid> Predict(Battle battle, int turnsInAdvance)
        { 
            var baseList = battle.Participants
                .Where(x => x.Enemy is null || x.Enemy.Health > 0)
                .Where(x => x.Character is null || x.Character.CurrentHealth > 0)
                .OrderBy(x => x.Order).ToList();

            var beforeCurrent = baseList.SkipWhile(x => x != battle.NextInTurn && battle.NextInTurn is not null);
            var result = new List<Guid>();
            result.AddRange(beforeCurrent.Select(x => x.Id));
            while (result.Count < turnsInAdvance) 
            {
                result.AddRange(baseList.Select(x => x.Id));
            }

            return result;
        }

        public BattleParticipant GetNext(Battle battle)
        {
            var baseList = battle.Participants
                .Where(x => x.Enemy is null || x.Enemy.Health > 0)
                .Where(x => x.Character is null || x.Character.CurrentHealth > 0)
                .OrderBy(x => x.Order).ToList();

            var nextParticipant = battle.NextInTurn;

            if (nextParticipant == null) 
            {
                battle.NextInTurn = baseList[0];
                battle.NextInTurn.CurrentActionGauge = battle.NextInTurn.ActionGauge;
                return battle.NextInTurn;
            }

            var afterCurrent = baseList.SkipUntil(x => x == nextParticipant).Concat(baseList).ToList();
            battle.NextInTurn = afterCurrent[0];
            battle.NextInTurn.CurrentActionGauge = battle.NextInTurn.ActionGauge;

            return battle.NextInTurn;
        }
    }
}
