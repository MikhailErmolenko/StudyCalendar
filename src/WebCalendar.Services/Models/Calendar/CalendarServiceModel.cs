using System;
using System.Collections.Generic;
using WebCalendar.Services.Models.Event;
using WebCalendar.Services.Models.Reminder;
using WebCalendar.Services.Models.Task;
using WebCalendar.Services.Models.User;

namespace WebCalendar.Services.Models.Calendar
{
    public class CalendarServiceModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public UserServiceModel User { get; set; }

        public ICollection<UserServiceModel> SubscribedUsers { get; set; }
        public ICollection<EventServiceModel> Events { get; set; }
        public ICollection<ReminderServiceModel> Reminders { get; set; }
        public ICollection<TaskServiceModel> Tasks { get; set; }
    }
}