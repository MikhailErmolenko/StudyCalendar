using System;
using System.Collections.Generic;
using WebCalendar.DAL.Models;
using WebCalendar.Services.Models.Calendar;

namespace WebCalendar.Services.Models.Reminder
{
    public class ReminderServiceModel : IRepeatableActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }

        public CalendarServiceModel Calendar { get; set; }
    }
}