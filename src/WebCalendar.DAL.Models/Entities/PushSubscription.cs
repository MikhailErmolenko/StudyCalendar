using System;

namespace WebCalendar.DAL.Models.Entities
{
    public class PushSubscription : IEntity
    {
        public Guid Id { get; set; }
        
        public string DeviceToken { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}