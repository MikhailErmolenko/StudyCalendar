using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Event;
using WebCalendar.Services.Scheduler.Contracts;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ISchedulerService _schedulerService;
        public EventService(IUnitOfWork uow, IMapper mapper, ISchedulerService schedulerService)
        {
            _uow = uow;
            _mapper = mapper;
            _schedulerService = schedulerService;
        }

        public async Task<EventServiceModel> AddAsync(EventCreationServiceModel entity)
        {
            Event @event = _mapper.Map<EventCreationServiceModel, Event>(entity);
            await _uow.GetRepository<Event>().AddAsync(@event);
            await _uow.SaveChangesAsync();

            await _schedulerService.ScheduleEventById(@event.Id);

            EventServiceModel eventServiceModel = _mapper.Map<Event, EventServiceModel>(@event);

            return eventServiceModel;
        }

        public async Task<IEnumerable<EventServiceModel>> GetAllAsync()
        {
            IEnumerable<Event> events = await _uow.GetRepository<Event>()
                .GetAllAsync();

            IEnumerable<EventServiceModel> eventServiceModels = _mapper
                .Map<IEnumerable<Event>, IEnumerable<EventServiceModel>>(events);

            return eventServiceModels;
        }

        public async Task<EventServiceModel> GetByIdAsync(Guid id)
        {
            Event @event = await _uow.GetRepository<Event>()
				 .GetByIdAsync(id);
			//.GetFirstOrDefaultAsync(
			//   predicate: e => e.Id == id,
			//   include: query => query
			//       .Include(e => e.Calendar));
			EventServiceModel eventServiceModel = _mapper.Map<Event, EventServiceModel>(@event);

            return eventServiceModel;
        }

        public async Task RemoveAsync(Guid id)
        {
            Event @event = await _uow.GetRepository<Event>()
           .GetByIdAsync(id);
            EventServiceModel eventServiceModel = _mapper
                .Map<Event, EventServiceModel>(@event);

            await _schedulerService.UnscheduleEventById(id);

            await RemoveAsync(eventServiceModel);

            //await RemoveAsync(eventServiceModel);

            //await _schedulerService.UnscheduleEventById(id);
        }

        public async Task RemoveAsync(EventServiceModel entity)
        {
            Event @event = await _uow.GetRepository<DAL.Models.Entities.Event>()
               .GetFirstOrDefaultAsync(
                    predicate: e => e.Id == entity.Id,
                    disableTracking: false);
        //Event @event = _mapper
        //    .Map<EventServiceModel, Event>(entity);
        _uow.GetRepository<Event>().Remove(@event);

            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(EventEditionServiceModel entity)
        {
            Event @event = _mapper
               .Map<EventEditionServiceModel, Event>(entity);
            _uow.GetRepository<Event>().Update(@event);

            await _uow.SaveChangesAsync();

            await _schedulerService.RescheduleEventById(entity.Id);
        }

        public async Task<IEnumerable<EventServiceModel>> GetAllByCalendarIdAsync(Guid calendarId)
		{
            IEnumerable<Event> events = await _uow.GetRepository<Event>()
            .GetAllAsync(e => e.CalendarId == calendarId);

            IEnumerable<EventServiceModel> eventServiceModels = _mapper
                .Map<IEnumerable<Event>, IEnumerable<EventServiceModel>>(events);

            return eventServiceModels;
        }

        public async Task<bool> ExistsAsync(Guid eventId)
		{
            bool exists = await _uow.GetRepository<Event>()
            .ExistsAsync(e => e.Id == eventId);

            return exists;
        }
    }
}