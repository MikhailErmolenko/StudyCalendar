using System;

namespace WebCalendar.DAL.Models.Entities
{
    public class UserEvent : ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}