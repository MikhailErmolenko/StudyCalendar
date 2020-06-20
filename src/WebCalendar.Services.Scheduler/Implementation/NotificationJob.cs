using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Models;

namespace WebCalendar.Services.Scheduler.Implementation
{
    [DisallowConcurrentExecution]
    public class NotificationJob : IJob
    {
        public static readonly string JobDataKey = Guid.NewGuid().ToString();
        public static readonly string JobActivityTypeKey = Guid.NewGuid().ToString();

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap jobDataMap = context.JobDetail.JobDataMap;
            
            string activityId = jobDataMap.GetString(JobDataKey);
            string activityType = jobDataMap.GetString(JobActivityTypeKey);
            
            
            switch (activityType)
            {
                case ConstantsStorage.TASK:
                {                 
                    using IServiceScope scope = _serviceScopeFactory.CreateScope();
                    INotificationService notificationService = scope.ServiceProvider.GetService<INotificationService>();
                        
                    await notificationService.SendTaskNotificationAsync(new Guid(activityId), NotificationType.Start);
                    
                    break;                  
                }

                case ConstantsStorage.EVENT:
                {
                    break;
                }

                case ConstantsStorage.REMINDER:
                {
                    break;
                }
            }
        }
    }
}