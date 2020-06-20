using System;
using System.Collections.Generic;

namespace WebCalendar.DAL.Models.Entities
{
    public class Calendar : IEntity, ISoftDeletable
    {
        public Calendar()
        {
            CalendarUsers = new HashSet<CalendarUser>();
            Events = new HashSet<Event>();
            Reminders = new HashSet<Reminder>();
            Tasks = new HashSet<Task>();
        }
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<CalendarUser> CalendarUsers { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}