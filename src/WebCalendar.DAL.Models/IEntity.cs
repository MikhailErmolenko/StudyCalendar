using System;

namespace WebCalendar.DAL.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime AddedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}