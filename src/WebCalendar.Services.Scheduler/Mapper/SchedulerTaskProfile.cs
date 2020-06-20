using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    class SchedulerTaskProfile : AutoMapperProfile
    {
        public SchedulerTaskProfile()
        {
            CreateMap<Task, SchedulerTask>();
        }
    }
}
