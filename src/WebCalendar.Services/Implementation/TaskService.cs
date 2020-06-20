using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Task;
using WebCalendar.Services.Scheduler;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ISchedulerService _schedulerService;

        public TaskService(IUnitOfWork uow, IMapper mapper, ISchedulerService schedulerService)
        {
            _uow = uow;
            _mapper = mapper;
            _schedulerService = schedulerService;
        }

        public async Task<TaskServiceModel> AddAsync(TaskCreationServiceModel entity)
        {
            DAL.Models.Entities.Task task = _mapper.Map<TaskCreationServiceModel, DAL.Models.Entities.Task>(entity);

            await _uow.GetRepository<DAL.Models.Entities.Task>().AddAsync(task);
            
            await _uow.SaveChangesAsync();

            await _schedulerService.ScheduleTaskById(task.Id);
            
            TaskServiceModel taskServiceModel = _mapper.Map<DAL.Models.Entities.Task, TaskServiceModel>(task);

            return taskServiceModel;
        }

        public async Task<IEnumerable<TaskServiceModel>> GetAllAsync()
        {
            IEnumerable<DAL.Models.Entities.Task> tasks = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetAllAsync();

            IEnumerable<TaskServiceModel> taskServiceModels = _mapper
                .Map<IEnumerable<DAL.Models.Entities.Task>, IEnumerable<TaskServiceModel>>(tasks);

            return taskServiceModels;
        }

        public async Task<TaskServiceModel> GetByIdAsync(Guid id)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetFirstOrDefaultAsync(
                    predicate: t => t.Id == id,
                    include: query => query
                        .Include(t => t.Calendar));
            TaskServiceModel taskServiceModel = _mapper.Map<DAL.Models.Entities.Task, TaskServiceModel>(task);

            return taskServiceModel;
        }

        public async Task RemoveAsync(Guid id)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetByIdAsync(id);
            TaskServiceModel taskServiceModel = _mapper
                .Map<DAL.Models.Entities.Task, TaskServiceModel>(task);

            await _schedulerService.UnscheduleTaskById(id);
            
            await RemoveAsync(taskServiceModel);
        }

        public async Task RemoveAsync(TaskServiceModel entity)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetFirstOrDefaultAsync(
                    predicate: t => t.Id == entity.Id,
                    disableTracking: false);

            _uow.GetRepository<DAL.Models.Entities.Task>().Remove(task);

            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskEditionServiceModel entity)
        {
            DAL.Models.Entities.Task task = _mapper
                .Map<TaskEditionServiceModel, DAL.Models.Entities.Task>(entity);
            _uow.GetRepository<DAL.Models.Entities.Task>().Update(task);

            await _uow.SaveChangesAsync();

            await _schedulerService.RescheduleTaskById(entity.Id);
        }

        public async Task<IEnumerable<TaskServiceModel>> GetAllByCalendarIdAsync(Guid calendarId)
        {
            IEnumerable<DAL.Models.Entities.Task> tasks = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetAllAsync(t => t.CalendarId == calendarId);

            IEnumerable<TaskServiceModel> taskServiceModels = _mapper
                .Map<IEnumerable<DAL.Models.Entities.Task>, IEnumerable<TaskServiceModel>>(tasks);

            return taskServiceModels;
        }

        public async Task<bool> ExistsAsync(Guid taskId)
        {
            bool exists = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .ExistsAsync(t => t.Id == taskId);

            return exists;
        }

        public async Task TaskCompletion(Guid taskId, bool isDone)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>().GetByIdAsync(taskId);
            
            task.IsDone = isDone;
            
            _uow.GetRepository<DAL.Models.Entities.Task>().Update(task);

            await _uow.SaveChangesAsync();
        }
    }
}