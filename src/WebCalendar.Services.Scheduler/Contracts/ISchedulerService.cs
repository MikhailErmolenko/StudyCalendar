using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface ISchedulerService
    {
        Task RescheduleEventById(Guid id);
        Task RescheduleReminderById(Guid id);
        Task RescheduleTaskById(Guid id);
        Task ScheduleEventById(Guid id);
        Task ScheduleReminderById(Guid id);
        Task ScheduleTaskById(Guid id);
        Task UnscheduleEventById(Guid id);
        Task UnscheduleReminderById(Guid id);
        Task UnscheduleTaskById(Guid id);
    }
}
