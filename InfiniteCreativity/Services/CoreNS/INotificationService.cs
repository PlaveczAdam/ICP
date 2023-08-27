using InfiniteCreativity.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface INotificationService
    {
        public Task SendFeNotification(int playerId, NotificationType notificationType);
        public Task OnFeConnected(HubCallerContext hubContext);
        public Task OnFeDisconnected(HubCallerContext hubContext);
        public Task SendFeNotificationToEveryone(NotificationType notificationType);

        public Task SendGNotification(int playerId);
        public Task OnGConnected(HubCallerContext hubContext);
        public Task OnGDisconnected(HubCallerContext hubContext);
        public Task SendGNotificationToEveryone();
    }
}
