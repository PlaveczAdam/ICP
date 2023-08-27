using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Services.CoreNS;

namespace InfiniteCreativity.HostedServices
{
    public class QuestScheduler : IHostedService
    {
        private CancellationTokenSource _tokenSource = new();
        private IServiceProvider _serviceProvider;

        public QuestScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(()=> Tick(_tokenSource.Token), _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }
        private async Task Tick(CancellationToken cancellationToken)
        { 
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope()) { 
                    var questService = scope.ServiceProvider.GetRequiredService<IQuestService>();
                    await questService.TickQuests();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    await notificationService.SendFeNotificationToEveryone(NotificationType.QuestUpdate);
                    await notificationService.SendGNotificationToEveryone();
                }
                    cancellationToken.WaitHandle.WaitOne(TimeSpan.FromMinutes(1));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
