using System;

namespace WebCalendar.Services.Models.Calendar
{
    public class CalendarCreationServiceModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }
    }
}