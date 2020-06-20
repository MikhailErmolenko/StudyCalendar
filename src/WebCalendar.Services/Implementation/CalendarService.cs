using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Calendar;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class CalendarService : ICalendarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public CalendarService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddAsync(CalendarCreationServiceModel entity)
        {
            Calendar calendar = _mapper
                .Map<CalendarCreationServiceModel, Calendar>(entity);
            await _uow.GetRepository<Calendar>().AddAsync(calendar);

            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<CalendarServiceModel>> GetAllAsync()
        {
            IEnumerable<Calendar> calendars = await _uow.GetRepository<Calendar>()
                .GetAllAsync();

            IEnumerable<CalendarServiceModel> calendarServiceModels = _mapper
                .Map<IEnumerable<Calendar>, IEnumerable<CalendarServiceModel>>(calendars);

            return calendarServiceModels;
        }

        public async Task<CalendarServiceModel> GetByIdAsync(Guid id)
        {
            Calendar calendar = await _uow.GetRepository<Calendar>()
                .GetByIdAsync(id);
            CalendarServiceModel calendarServiceModel = _mapper
                .Map<Calendar, CalendarServiceModel>(calendar);

            return calendarServiceModel;
        }

        public async Task RemoveAsync(Guid id)
        {
            Calendar calendar = await _uow.GetRepository<Calendar>()
                .GetByIdAsync(id);
            CalendarServiceModel calendarServiceModel = _mapper
                .Map<Calendar, CalendarServiceModel>(calendar);

            await RemoveAsync(calendarServiceModel);
        }

        public async Task RemoveAsync(CalendarServiceModel entity)
        {
            Calendar calendar = _mapper
                .Map<CalendarServiceModel, Calendar>(entity);
            _uow.GetRepository<Calendar>().Remove(calendar);

            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(CalendarEditionServiceModel entity)
        {
            Calendar calendar = _mapper
                .Map<CalendarEditionServiceModel, Calendar>(entity);
            _uow.GetRepository<Calendar>().Update(calendar);

            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<CalendarServiceModel>> GetAllUserCalendarsAsync(Guid userId)
        {
            IEnumerable<Calendar> calendars = await _uow.GetRepository<Calendar>()
                .GetAllAsync(calendar => calendar.UserId == userId);

            IEnumerable<CalendarServiceModel> userCalendars = _mapper
                .Map<IEnumerable<Calendar>, IEnumerable<CalendarServiceModel>>(calendars);
            
            return userCalendars;
        }

        public async Task ShareToUser(Guid calendarId, Guid userId)
        {
            var calendarUser =
                new CalendarUser
                {
                    CalendarId = calendarId,
                    UserId = userId
                };
            await _uow.GetRepository<CalendarUser>()
                .AddAsync(calendarUser);

            await _uow.SaveChangesAsync();
        }

        public async Task UnshareToUser(Guid calendarId, Guid userId)
        {
            CalendarUser calendarUser = await _uow.GetRepository<CalendarUser>()
                .GetFirstOrDefaultAsync(predicate: cu => cu.UserId == userId
                                              && cu.CalendarId == calendarId);
            _uow.GetRepository<CalendarUser>().Remove(calendarUser);

            await _uow.SaveChangesAsync();
        }
    }
}