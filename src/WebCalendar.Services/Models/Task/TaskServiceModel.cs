using System;
using WebCalendar.DAL.Models;
using WebCalendar.Services.Models.Calendar;

namespace WebCalendar.Services.Models.Task
{
    public class TaskServiceModel : IActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }

        public Guid CalendarId { get; set; }
        public CalendarServiceModel Calendar { get; set; }
    }
}