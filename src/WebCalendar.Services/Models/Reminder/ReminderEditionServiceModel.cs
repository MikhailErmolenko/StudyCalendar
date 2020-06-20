using System;
using System.Collections.Generic;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Models.Reminder
{
    public class ReminderEditionServiceModel : IRepeatableActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }
    }
}