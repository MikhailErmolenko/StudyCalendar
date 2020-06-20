using WebCalendar.Common;
using WebCalendar.Services.Models.Calendar;
using WebCalendar.WebApi.Models.Calendar;

namespace WebCalendar.WebApi.Mapper
{
    public class CalendarModelProfile : AutoMapperProfile
    {
        public CalendarModelProfile()
        {
            CreateMap<CalendarServiceModel, CalendarInfoModel>();
        }
    }
}