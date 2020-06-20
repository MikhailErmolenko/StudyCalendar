using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface ISchedulerActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
    }
}
