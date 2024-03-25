using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Notification.Hubs;
using TableDependency.SqlClient;

namespace Nakheel_Web.Notification.SubscribeTableDependencies
{
    [TypeFilter(typeof(ExpFilter))]
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<tbl_Notification_Sequence>? tableDependency;
        NotificationHub NotificationsHub;

        public SubscribeNotificationTableDependency(NotificationHub NotificationHub)
        {
            this.NotificationsHub = NotificationHub;

        }
        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<tbl_Notification_Sequence>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }
        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<tbl_Notification_Sequence> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await NotificationsHub.SendNotification();
            }
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(tbl_Notification_Sequence)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
