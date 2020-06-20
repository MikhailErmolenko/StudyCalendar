using System;

namespace WebCalendar.WebApi.Models.Task
{
    public class TaskCompletionModel
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
    }
}