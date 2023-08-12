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

        public async Task SendNotification(int playerId, NotificationType notificationType)
        {
            var whom = await _playerService.GetPlayerById(playerId);
            var connections = whom.Connections.Where((x) => x.Connected).ToList();
            foreach (var connection in connections)
            {
                await _hubContext.Clients
                    .Client(connection.ConnectionID)
                    .SendAsync("Notification", notificationType.ToString());
            }
            
        }
        public async Task SendNotificationToEveryone(NotificationType notificationType)
        {
            await _hubContext.Clients.All.SendAsync("Notification", notificationType.ToString());
        }

        public async Task OnConnected(HubCallerContext hubContext)
        {
            var currentPlyer = await _playerService.GetCurrentPlayer(withConnections:true);

            currentPlyer.Connections.Add(new Connection
                {
                    ConnectionID = hubContext.ConnectionId,
                    UserAgent = hubContext.GetHttpContext().Request.Headers["User-Agent"],
                    Connected = true
                });
            await _context.SaveChangesAsync();
        }

        public async Task OnDisconnected(HubCallerContext hubContext)
        {
            var connection = _context.Connection.Find(hubContext.ConnectionId);
            if (connection is not null)
            {
                _context.Remove(connection);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
