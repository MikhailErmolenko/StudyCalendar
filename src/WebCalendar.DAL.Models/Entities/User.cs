using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebCalendar.DAL.Models.Entities
{
    public class User : IdentityUser<Guid>, IEntity, ISoftDeletable
    {   
        public User()
        {
            CalendarUsers = new HashSet<CalendarUser>();
            Calendars = new HashSet<Calendar>();
            UserEvents = new HashSet<UserEvent>();
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }

        public bool IsSubscribedToEmailNotifications { get; set; } = true;
        public bool IsDeleted { get; set; }

        public ICollection<CalendarUser> CalendarUsers { get; set; }
        public ICollection<Calendar> Calendars { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }

        public ICollection<PushSubscription> PushSubscriptions { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}