using System;
using WebCalendar.Services.Scheduler.Contracts;

namespace WebCalendar.Services.Scheduler.Models
{
    public class SchedulerEvent : ISchedulerRepeatableActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CronExpression { get; set; }
    }
}
