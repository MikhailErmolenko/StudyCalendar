using System;
using System.Collections.Generic;

namespace WebCalendar.DAL.Models.Entities
{
    public class Event : IEntity, IRepeatableActivity, ISoftDeletable
    {
        public Event()
        {
            UserEvents = new HashSet<UserEvent>();
            DaysOfWeek = new HashSet<int>();
            DaysOfMounth = new HashSet<int>();
            Monthes = new HashSet<int>();
            Years = new HashSet<int>();
        }

        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }

        public Guid CalendarId { get; set; }
        public Calendar Calendar { get; set; }

        public ICollection<UserEvent> UserEvents { get; set; }
    }
}