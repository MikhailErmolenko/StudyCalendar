using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Services.Models.Reminder;

namespace WebCalendar.Services.Contracts
{
    public interface IReminderService
    {
        Task AddAsync(ReminderCreationServiceModel entity);
        Task<IEnumerable<ReminderServiceModel>> GetAllAsync();
        Task<ReminderServiceModel> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(ReminderServiceModel entity);
        Task UpdateAsync(ReminderEditionServiceModel entity);
    }
}