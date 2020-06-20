using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;
using Task = WebCalendar.DAL.Models.Entities.Task;

namespace WebCalendar.Services.Scheduler.Implementation
{
    public class SchedulerDataLoader : ISchedulerDataLoader
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IScheduler _scheduler;

        public SchedulerDataLoader(IUnitOfWork uow, IMapper mapper, IQuartzHostedService quartzService)
        {
            _uow = uow;
            _mapper = mapper;
            _scheduler = quartzService.Scheduler;
        }
        
        public async void Initialize()
        {
            IEnumerable<SchedulerEvent> events = await GetSchedulerEvents();
            IEnumerable<SchedulerReminder> reminders = await GetSchedulerReminders();
            IEnumerable<SchedulerTask> tasks = await GetSchedulerTasks();

            foreach (var @event in @events)
            {
                await _scheduler.ScheduleEvent(@event);
            }

            foreach (var reminder in reminders)
            {
                await _scheduler.ScheduleReminder(reminder);
            }

            foreach (var task in tasks)
            {
                await _scheduler.ScheduleTask(task);
            }
        }

        private async Task<IEnumerable<SchedulerEvent>> GetSchedulerEvents()
        {
            IEnumerable<Event> events = await _uow.GetRepository<Event>().GetAllAsync();
            IEnumerable<SchedulerEvent> schedulerEvents = _mapper.Map<IEnumerable<Event>, IEnumerable<SchedulerEvent>>(events);

            return schedulerEvents;
        }

        private async Task<IEnumerable<SchedulerReminder>> GetSchedulerReminders()
        {
            IEnumerable<Reminder> reminders = await _uow.GetRepository<Reminder>().GetAllAsync();
            IEnumerable<SchedulerReminder> schedulerReminders = _mapper.Map<IEnumerable<Reminder>, IEnumerable<SchedulerReminder>>(reminders);

            return schedulerReminders;
        }

        private async Task<IEnumerable<SchedulerTask>> GetSchedulerTasks()
        {
            IEnumerable<Task> tasks = await _uow.GetRepository<Task>().GetAllAsync();
            IEnumerable<SchedulerTask> schedulerTasks = _mapper.Map<IEnumerable<Task>, IEnumerable<SchedulerTask>>(tasks);

            return schedulerTasks;
        }
    }
}
