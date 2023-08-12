using InfiniteCreativity.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Services
{
    public interface INotificationService
    {
        public Task SendNotification(int playerId, NotificationType notificationType);
        public Task OnConnected(HubCallerContext hubContext);
        public Task OnDisconnected(HubCallerContext hubContext);
        public Task SendNotificationToEveryone(NotificationType notificationType);
    }
}
