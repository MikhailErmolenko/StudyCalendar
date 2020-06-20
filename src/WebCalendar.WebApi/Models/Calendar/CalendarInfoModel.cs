using System;

namespace WebCalendar.WebApi.Models.Calendar
{
    public class CalendarInfoModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}