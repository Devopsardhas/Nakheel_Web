using Microsoft.AspNetCore.SignalR;
using Nakheel_Web.Notification.Repository;

namespace Nakheel_Web.Notification.Hubs
{
    public class NotificationHub : Hub
    {
        BellNotificationRepo BellRepository;

        public NotificationHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NotificationConnection");
            BellRepository = new BellNotificationRepo(connectionString);

        }
        public async Task SendNotification()
        {
            await Clients.All.SendAsync("ReceivedNotification");

        }
    }
}
