using System;
using System.Collections.Generic;
using WebCalendar.Services.Models.User;

namespace WebCalendar.WebApi.Models.Event
{
	public class EventCreationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }

        public Guid CalendarId { get; set; }
        public ICollection<UserSummaryModel> SubscribedUsers { get; set; }
    }
}
