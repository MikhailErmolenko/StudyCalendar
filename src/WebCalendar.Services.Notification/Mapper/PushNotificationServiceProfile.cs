using WebCalendar.Common;
using WebCalendar.Services.Notification.Models;

namespace WebCalendar.Services.Notification.Mapper
{
    public class PushNotificationServiceProfile : AutoMapperProfile
    {
        public PushNotificationServiceProfile()
        {
            CreateMap<PushNotificationServiceModel, PushNotification.Models.Notification>();
        }
    }
}