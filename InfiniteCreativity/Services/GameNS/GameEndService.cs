﻿using AutoMapper;
using InfiniteCreativity.Data;
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

        public async Task Endgame(string gConnectionId)
        {
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
            _context.GConnection.Remove(gconn);
        }
    }
}