using WebCalendar.Common;
using WebCalendar.Services.Models.Event;
using WebCalendar.WebApi.Models.Event;

namespace WebCalendar.WebApi.Mapper
{
    public class EventModelProfile : AutoMapperProfile
    {
        public EventModelProfile()
        {
            CreateMap<EventCreationModel, EventCreationServiceModel>();
            CreateMap<EventServiceModel, EventModel>();
        }
    }
}