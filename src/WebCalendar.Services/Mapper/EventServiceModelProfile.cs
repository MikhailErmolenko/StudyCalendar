using System.Linq;
using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Models.Event;

namespace WebCalendar.Services.Mapper
{
    public class EventServiceModelProfile : AutoMapperProfile
    {
        public EventServiceModelProfile()//Без понятия нормально ли оно маппит события
        {
            CreateMap<EventCreationServiceModel, Event>()
                .ForMember(ue => ue.UserEvents,
                    o => o.MapFrom(c => c.SubscribedUsers.Select(su =>
                        new UserEvent
                        {
                            UserId = su.Id //NEED TO TEST IT 
                        })));


            CreateMap<EventEditionServiceModel, Event>()
                .ForMember(ue => ue.UserEvents,
                    o => o.MapFrom(c => c.SubscribedUsers.Select(su =>
                        new UserEvent
                        {
                            UserId = su.Id,
                            EventId = c.Id
                        })));

            CreateMap<Event, EventServiceModel>()
                .ForMember(e => e.SubscribedUsers,
                    o => o.MapFrom(e => e.UserEvents.Select(ue => ue.User)));

            CreateMap<EventServiceModel, Event>()
                .ForMember(ue => ue.UserEvents,
                    o => o.MapFrom(c => c.SubscribedUsers.Select(su =>
                        new UserEvent
                        {
                            UserId = su.Id,
                            EventId = c.Id
                        })));
        }
    }
}