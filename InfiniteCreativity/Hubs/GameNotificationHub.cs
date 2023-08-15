using InfiniteCreativity.Services;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Hubs
{
    public class GameNotificationHub : Hub
    {
        private INotificationService _notificationService;

        public GameNotificationHub(INotificationService notificationService)
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
