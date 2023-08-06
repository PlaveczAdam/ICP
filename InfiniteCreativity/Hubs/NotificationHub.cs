using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Components;

namespace InfiniteCreativity.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async override Task OnConnectedAsync()
        {
            await _notificationService.OnConnected(Context);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await _notificationService.OnDisconnected(Context);
            await base.OnDisconnectedAsync(exception);
        }
    }
}