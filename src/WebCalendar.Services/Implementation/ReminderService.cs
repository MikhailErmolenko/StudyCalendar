using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Reminder;
using WebCalendar.Services.Scheduler.Contracts;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class ReminderService : IReminderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ISchedulerService _schedulerService;

        public ReminderService(IUnitOfWork uow, IMapper mapper, ISchedulerService schedulerService)
        {
            _uow = uow;
            _mapper = mapper;
            _schedulerService = schedulerService;
        }

        public async Task AddAsync(ReminderCreationServiceModel entity)
        {
            Reminder reminder = _mapper.Map<ReminderCreationServiceModel, Reminder>(entity);
            await _uow.GetRepository<Reminder>().AddAsync(reminder);

            await _uow.SaveChangesAsync();

            // await _schedulerService.ScheduleReminderById()
        }

        public async Task<IEnumerable<ReminderServiceModel>> GetAllAsync()
        {
            IEnumerable<Reminder> reminders = await _uow.GetRepository<Reminder>()
                .GetAllAsync();

            IEnumerable<ReminderServiceModel> reminderServiceModels = _mapper
                .Map<IEnumerable<Reminder>, IEnumerable<ReminderServiceModel>>(reminders);

            return reminderServiceModels;
        }

        public async Task<ReminderServiceModel> GetByIdAsync(Guid id)
        {
            Reminder reminder = await _uow.GetRepository<Reminder>()
                .GetByIdAsync(id);
            ReminderServiceModel reminderServiceModel = _mapper.Map<Reminder, ReminderServiceModel>(reminder);

            return reminderServiceModel;
        }

        public async Task RemoveAsync(Guid id)
        {
            Reminder reminder = await _uow.GetRepository<Reminder>()
                .GetByIdAsync(id);
            ReminderServiceModel reminderServiceModel = _mapper
                .Map<Reminder, ReminderServiceModel>(reminder);

            await RemoveAsync(reminderServiceModel);
        }

        public async Task RemoveAsync(ReminderServiceModel entity)
        {
            Reminder reminder = _mapper
                .Map<ReminderServiceModel, Reminder>(entity);
            _uow.GetRepository<Reminder>().Remove(reminder);

            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReminderEditionServiceModel entity)
        {
            Reminder reminder = _mapper
                .Map<ReminderEditionServiceModel, Reminder>(entity);
            _uow.GetRepository<Reminder>().Update(reminder);

            await _uow.SaveChangesAsync();
        }
    }
}