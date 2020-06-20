using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    class SchedulerEventProfile : AutoMapperProfile
    {
        public SchedulerEventProfile()
        {
            CreateMap<Event, SchedulerEvent>()
               .ForMember(e => e.CronExpression,
                    o => o.MapFrom(e => e.GetCronExpression()));
        }
    }
}
