using System;

namespace WebCalendar.Services.Models.User
{
    public class UserSummaryModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}