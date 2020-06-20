using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    class SchedulerReminderProfile : AutoMapperProfile
    {
        public SchedulerReminderProfile()
        {
            CreateMap<Reminder, SchedulerReminder>()
               .ForMember(e => e.CronExpression,
                    o => o.MapFrom(e => e.GetCronExpression()));
        }
    }
}
