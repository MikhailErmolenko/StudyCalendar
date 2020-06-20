using System;
using System.Threading.Tasks;
using WebCalendar.Services.Notification.Models;

namespace WebCalendar.Services.Notification.Contracts
{
    public interface INotificationService
    {
        public Task SendTaskNotificationAsync(Guid taskId, NotificationType type);
    }
}