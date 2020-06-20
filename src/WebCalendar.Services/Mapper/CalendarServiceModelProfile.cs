using System.Linq;
using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Models.Calendar;

namespace WebCalendar.Services.Mapper
{
    public class CalendarServiceModelProfile : AutoMapperProfile
    {
        public CalendarServiceModelProfile()
        {
            CreateMap<CalendarCreationServiceModel, Calendar>();

            CreateMap<CalendarEditionServiceModel, Calendar>();

            CreateMap<Calendar, CalendarServiceModel>()
                .ForMember(c => c.SubscribedUsers,
                    o => o.MapFrom(c => c.CalendarUsers.Select(cu => cu.User)));

            CreateMap<CalendarServiceModel, Calendar>()
                .ForMember(c => c.CalendarUsers, o => o
                    .MapFrom(c => c.SubscribedUsers.Select(su =>
                        new CalendarUser
                        {
                            UserId = su.Id,
                            CalendarId = c.Id
                        })));
        }
    }
}