using System.Linq;
using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Models.Reminder;

namespace WebCalendar.Services.Mapper
{
    public class ReminderServiceModelProfile : AutoMapperProfile
    {
        public ReminderServiceModelProfile()
        {
            CreateMap<ReminderCreationServiceModel, Reminder>();

            CreateMap<ReminderEditionServiceModel, Reminder>();

            CreateMap<Reminder, ReminderServiceModel>();

            CreateMap<ReminderServiceModel, Reminder>();
        }
    }
}