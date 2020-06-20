using System;
using System.Threading.Tasks;
using Quartz;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.DAL.Repositories.Contracts;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;
using Task = WebCalendar.DAL.Models.Entities.Task;

namespace WebCalendar.Services.Scheduler.Implementation
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IAsyncRepository<Task> _taskRepository;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Reminder> _reminderRepository;
        private readonly IMapper _mapper;
        private readonly IScheduler _scheduler;
        
        public SchedulerService(IUnitOfWork uow, IMapper mapper, IQuartzHostedService quartzService)
        {
            _taskRepository = uow.GetRepository<Task>();
            _eventRepository = uow.GetRepository<Event>();
            _reminderRepository = uow.GetRepository<Reminder>();

            _mapper = mapper;

            _scheduler = quartzService.Scheduler;
        }

        public async System.Threading.Tasks.Task ScheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.ScheduleTask(schedulerTask);         
        }

        public async System.Threading.Tasks.Task UnscheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.UnscheduleTask(schedulerTask);
        }

        public async System.Threading.Tasks.Task RescheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.RescheduleTask(schedulerTask);
        }

        public async System.Threading.Tasks.Task UnscheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.UnscheduleEvent(schedulerEvent);
        }

        public async System.Threading.Tasks.Task RescheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.RescheduleEvent(schedulerEvent);
        }

        public async System.Threading.Tasks.Task ScheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.ScheduleEvent(schedulerEvent);
        }

        public async System.Threading.Tasks.Task ScheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.ScheduleReminder(schedulerReminder);
        }

        public async System.Threading.Tasks.Task UnscheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.UnscheduleReminder(schedulerReminder);
        }

        public async System.Threading.Tasks.Task RescheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.RescheduleReminder(schedulerReminder);
        }

        private async Task<SchedulerTask> GetTask(Guid id)
        {
            Task task = await _taskRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id);

            SchedulerTask schedulerTask = _mapper.Map<Task, SchedulerTask>(task);

            return schedulerTask;
        }

        private async Task<SchedulerEvent> GetEvent(Guid id)
        {
            Event @event = await _eventRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id);

            SchedulerEvent schedulerEvent = _mapper.Map<Event, SchedulerEvent>(@event);

            return schedulerEvent;
        }

        private async Task<SchedulerReminder> GetReminder(Guid id)
        {
            Reminder reminder = await _reminderRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id);

            SchedulerReminder schedulerReminder = _mapper.Map<Reminder, SchedulerReminder>(reminder);

            return schedulerReminder;
        }
    }
}
