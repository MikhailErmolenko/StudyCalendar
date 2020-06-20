using System;

namespace WebCalendar.WebApi.Models.Task
{
    public class TaskCreationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public Guid CalendarId { get; set; }
    }
}