using System.Collections.Generic;
using WebCalendar.PushNotification.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.PushNotification.Contracts
{
    public interface IPushNotificationSender
    {
        Task SendPushNotificationAsync(IEnumerable<string> deviceTokens,
            Notification notification);
    }
}