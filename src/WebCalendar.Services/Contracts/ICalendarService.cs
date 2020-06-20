using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Services.Models.Calendar;

namespace WebCalendar.Services.Contracts
{
    public interface ICalendarService
    {
        Task AddAsync(CalendarCreationServiceModel entity);
        Task<IEnumerable<CalendarServiceModel>> GetAllAsync();
        Task<CalendarServiceModel> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(CalendarServiceModel entity);
        Task ShareToUser(Guid calendarId, Guid userId);
        Task UnshareToUser(Guid calendarId, Guid userId);
        Task UpdateAsync(CalendarEditionServiceModel entity);

        Task<IEnumerable<CalendarServiceModel>> GetAllUserCalendarsAsync(Guid userId);
    }
}