using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Services.Models.Event;

namespace WebCalendar.Services.Contracts
{
    public interface IEventService
    {
        Task<EventServiceModel> AddAsync(EventCreationServiceModel entity);
        Task<IEnumerable<EventServiceModel>> GetAllAsync();
        Task<EventServiceModel> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(EventServiceModel entity);
        Task UpdateAsync(EventEditionServiceModel entity);
    }
}