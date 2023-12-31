﻿using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Hubs;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Services.GameNS;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Services.CoreNS
{
    public class NotificationService : INotificationService
    {
        private InfiniteCreativityContext _context;
        private IMapper _mapper;
        private IPlayerService _playerService;
        private IHubContext<NotificationHub> _hubContext;
        private IHubContext<GameNotificationHub> _gameNotificationHub;
        private IGameEndService _gameEndService;

        public NotificationService(IPlayerService playerService, IHubContext<NotificationHub> hubContext, InfiniteCreativityContext context, IHubContext<GameNotificationHub> gameNotificationHub, IGameEndService gameEndService)
        {
            _playerService = playerService;
            _hubContext = hubContext;
            _context = context;
            _gameNotificationHub = gameNotificationHub;
            _gameEndService = gameEndService;
        }

        public async Task SendFeNotification(Guid playerId, NotificationType notificationType)
        {
            var whom = await _playerService.GetPlayerById(playerId);
            var connections = whom.FeConnections.Where((x) => x.Connected).ToList();
            foreach (var connection in connections)
            {
                await _hubContext.Clients
                    .Client(connection.ConnectionID)
                    .SendAsync("Notification", notificationType.ToString());
            }

        }
        public async Task SendFeNotificationToEveryone(NotificationType notificationType)
        {
            await _hubContext.Clients.All.SendAsync("Notification", notificationType.ToString());
        }

        public async Task OnFeConnected(HubCallerContext hubContext)
        {
            var currentPlyer = await _playerService.GetCurrentPlayer(withFeConnections: true);

            currentPlyer.FeConnections.Add(new FeConnection
            {
                ConnectionID = hubContext.ConnectionId,
                UserAgent = hubContext.GetHttpContext().Request.Headers["User-Agent"],
                Connected = true
            });
            await _context.SaveChangesAsync();
        }

        public async Task OnFeDisconnected(HubCallerContext hubContext)
        {
            var connection = _context.FeConnection.FirstOrDefault(x => x.ConnectionID == hubContext.ConnectionId);
            if (connection is not null)
            {
                _context.Remove(connection);
            }

            await _context.SaveChangesAsync();
        }

        public async Task OnGConnected(HubCallerContext hubContext)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);

            foreach (var gconn in currentPlayer.GConnections)
            {
                await _gameEndService.Endgame(gconn.ConnectionID);
            }
            await _context.SaveChangesAsync();

            currentPlayer.GConnections.Add(new GConnection
            {
                ConnectionID = hubContext.ConnectionId
            });
            await _context.SaveChangesAsync();
        }

        public async Task OnGDisconnected(HubCallerContext hubContext)
        {
            await _gameEndService.Endgame(hubContext.ConnectionId);
            await _context.SaveChangesAsync();
        }

        public async Task SendGNotification(Guid playerId)
        {
            var whom = await _playerService.GetPlayerById(playerId);
            var connections = whom.GConnections;
            foreach (var connection in connections)
            {
                await _gameNotificationHub.Clients
                    .Client(connection.ConnectionID)
                    .SendAsync("Notification");
            }
        }

        public async Task SendGNotificationToEveryone()
        {
            await _gameNotificationHub.Clients.All.SendAsync("Notification");
        }
    }
}
