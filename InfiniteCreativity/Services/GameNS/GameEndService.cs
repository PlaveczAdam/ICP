using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Models.GameNS;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services.GameNS
{
    public class GameEndService:IGameEndService
    {
        private InfiniteCreativityContext _context;

        public GameEndService(InfiniteCreativityContext context)
        {
            _context = context;
        }

        public async Task Endgame(string gConnectionId, bool removeGameObjectsOnly = false)
        {
            GConnection copy = null;
            if (removeGameObjectsOnly)
            { 
                copy = _context.GConnection.AsNoTracking().FirstOrDefault(x => x.ConnectionID == gConnectionId);
                if (copy is not null)
                { 
                    copy.Id = Guid.NewGuid();
                    copy.Turn = 1;
                }
            }    

            var gconn = _context.GConnection
               .Include(x => x.Characters)
               .Include(x => x.Battle)
                    .ThenInclude(x => x.Participants)
                        .ThenInclude(x => x.Enemy)
               .Include(x => x.Map)
                    .ThenInclude(x => x.HexTiles)
                        .ThenInclude(x => x.Enemy)
               .Include(x => x.Map)
                    .ThenInclude(x => x.HexTiles)
                        .ThenInclude(x => x.DetailEntity)
               .FirstOrDefault(x => x.ConnectionID == gConnectionId);
            if (gconn.Battle is not null)
            {
                gconn.Battle.NextInTurnId = null;
                gconn.Battle.NextInTurn = null;
                await _context.SaveChangesAsync();
                _context.RemoveRange(gconn.Battle.Participants);
                await _context.SaveChangesAsync();
            }
            _context.GConnection.Remove(gconn);
            if (copy is not null)
            {
                _context.Add(copy);
            }
        }
    }
}
