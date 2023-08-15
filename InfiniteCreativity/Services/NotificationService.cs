using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Hubs;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace InfiniteCreativity.Services
{
    public class NotificationService : INotificationService
    {
        private InfiniteCreativityContext _context;
        private IMapper _mapper;
        private IPlayerService _playerService;
        private IHubContext<NotificationHub> _hubContext;

        public NotificationService(IPlayerService playerService, IHubContext<NotificationHub> hubContext, InfiniteCreativityContext context)
        {
            _playerService = playerService;
            _hubContext = hubContext;
            _context = context;
        }

        public async Task SendFeNotification(int playerId, NotificationType notificationType)
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
            var currentPlyer = await _playerService.GetCurrentPlayer(withFeConnections:true);

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
            var connection = _context.FeConnection.Find(hubContext.ConnectionId);
            if (connection is not null)
            {
                _context.Remove(connection);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task OnGConnected(HubCallerContext hubContext)
        {
            var currentPlyer = await _playerService.GetCurrentPlayer(withGConnections: true);

            currentPlyer.GConnections.Add(new GConnection
            {
                ConnectionID = hubContext.ConnectionId
            });
            await _context.SaveChangesAsync();
        }

        public async Task OnGDisconnected(HubCallerContext hubContext)
        {
            var connection = _context.GConnection.Find(hubContext.ConnectionId);
            if (connection is not null)
            {
                _context.Remove(connection);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SendGNotification(int playerId)
        {
            var whom = await _playerService.GetPlayerById(playerId);
            var connections = whom.GConnections;
            foreach (var connection in connections)
            {
                await _hubContext.Clients
                    .Client(connection.ConnectionID)
                    .SendAsync("Notification");
            }
        }

        public async Task SendGNotificationToEveryone()
        {
            await _hubContext.Clients.All.SendAsync("Notification");
        }
    }
}
