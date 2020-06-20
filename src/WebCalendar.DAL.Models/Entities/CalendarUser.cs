using System;

namespace WebCalendar.DAL.Models.Entities
{
    public class CalendarUser : ISoftDeletable
    {
        public bool IsDeleted { get; set; }

        public Guid CalendarId { get; set; }
        public Calendar Calendar { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}