using System;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Models.Task
{
    public class TaskEditionServiceModel : IActivity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }
    }
}