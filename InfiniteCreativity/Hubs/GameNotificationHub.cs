using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Hubs
{
    [Authorize]
    public class GameNotificationHub : Hub
    {
        private INotificationService _notificationService;

        public GameNotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async override Task OnConnectedAsync()
        {
            await _notificationService.OnGConnected(Context);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await _notificationService.OnGDisconnected(Context);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
