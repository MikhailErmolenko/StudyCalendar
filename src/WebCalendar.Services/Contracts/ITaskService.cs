using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Services.Models.Task;

namespace WebCalendar.Services.Contracts
{
    public interface ITaskService
    {
        Task<TaskServiceModel> AddAsync(TaskCreationServiceModel entity);
        Task<IEnumerable<TaskServiceModel>> GetAllAsync();
        Task<TaskServiceModel> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(TaskServiceModel entity);
        Task UpdateAsync(TaskEditionServiceModel entity);

        Task<IEnumerable<TaskServiceModel>> GetAllByCalendarIdAsync(Guid calendarId);

        Task<bool> ExistsAsync(Guid taskId);
        Task TaskCompletion(Guid taskId, bool isDone);
    }
}