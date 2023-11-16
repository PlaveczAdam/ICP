using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using InfiniteCreativity.Services.CoreNS;

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
            await _notificationService.OnFeConnected(Context);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await _notificationService.OnFeDisconnected(Context);
            await base.OnDisconnectedAsync(exception);
        }
    }
}