using System;
using WebCalendar.Services.Scheduler.Contracts;

namespace WebCalendar.Services.Scheduler.Models
{
    public class SchedulerTask : ISchedulerActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }
    }
}
