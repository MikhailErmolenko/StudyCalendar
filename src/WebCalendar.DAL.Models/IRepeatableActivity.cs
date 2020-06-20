using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.DAL.Models
{
    public interface IRepeatableActivity : IActivity
    {
        ICollection<int> DaysOfWeek { get; set; }
        ICollection<int> DaysOfMounth { get; set; }
        ICollection<int> Monthes { get; set; }
        ICollection<int> Years { get; set; }
    }
}
