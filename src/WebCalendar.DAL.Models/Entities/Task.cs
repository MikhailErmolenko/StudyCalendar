using System;

namespace WebCalendar.DAL.Models.Entities
{
    public class Task  : IEntity, IActivity, ISoftDeletable
    {
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }

        public Guid CalendarId { get; set; }
        public Calendar Calendar { get; set; }
    }
}