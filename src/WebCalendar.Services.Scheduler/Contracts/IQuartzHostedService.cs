using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface IQuartzHostedService : IHostedService
    {
        IScheduler Scheduler { get; set; }
    }
}
