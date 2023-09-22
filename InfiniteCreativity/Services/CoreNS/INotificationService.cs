using InfiniteCreativity.Models.Enums.CoreNS;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface INotificationService
    {
        public Task SendFeNotification(Guid playerId, NotificationType notificationType);
        public Task OnFeConnected(HubCallerContext hubContext);
        public Task OnFeDisconnected(HubCallerContext hubContext);
        public Task SendFeNotificationToEveryone(NotificationType notificationType);

        public Task SendGNotification(Guid playerId);
        public Task OnGConnected(HubCallerContext hubContext);
        public Task OnGDisconnected(HubCallerContext hubContext);
        public Task SendGNotificationToEveryone();
    }
}
