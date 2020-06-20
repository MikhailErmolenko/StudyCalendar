using System.Threading;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using WebCalendar.Services.Scheduler.Contracts;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Scheduler.Implementation
{
    public class QuartzHostedService : IQuartzHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;

        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public IScheduler Scheduler { get; set; }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            await Scheduler.Start(cancellationToken);
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}