using System;
using System.Collections.Generic;
using System.Diagnostics;
using CorePush.Google;
using WebCalendar.PushNotification.Contracts;
using WebCalendar.PushNotification.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.PushNotification.Implementation
{
    public class PushNotificationSender : IPushNotificationSender
    {
        private readonly FirebaseNotification _firebaseNotification;

        public PushNotificationSender(FirebaseNotification firebaseNotification)
        {
            _firebaseNotification = firebaseNotification;
        }

        public async Task SendPushNotificationAsync(IEnumerable<string> deviceTokens,
            Notification notification)
        {
            using var fcm = new FcmSender(_firebaseNotification.ServerKey, _firebaseNotification.SenderId);
            foreach (string token in deviceTokens)
            {
                FcmResponse fcmResponse = await fcm.SendAsync(token, new
                {
                    notification = new
                    {
                        title = notification.Title,
                        body = notification.Message,
                        click_action = notification.Url
                    },
                });
            }
        }
    }
}
