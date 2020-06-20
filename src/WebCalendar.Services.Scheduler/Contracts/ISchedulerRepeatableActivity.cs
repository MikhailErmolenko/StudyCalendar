using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface ISchedulerRepeatableActivity : ISchedulerActivity
    {
        public string CronExpression { get; set; }
    }
}
