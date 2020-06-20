using System;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WebCalendar.Common;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.EF.Context;
using WebCalendar.DAL.EF.Initializer;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.DAL.Repositories.Contracts;
using WebCalendar.DAL.Repositories.Implementation;
using WebCalendar.EmailSender;
using WebCalendar.EmailSender.Contracts;
using WebCalendar.PushNotification;
using WebCalendar.PushNotification.Contracts;
using WebCalendar.PushNotification.Implementation;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Export.Contracts;
using WebCalendar.Services.Export.Implementation;
using WebCalendar.Services.Implementation;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Implementation;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Implementation;

namespace WebCalendar.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration["ConnectionString"];

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection,
                    builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); }));

            services.AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireUppercase = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDataInitializer, EFDataInitializer>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepositoryAsync<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IMapper, WebCalendarAutoMapper>();

            var firebaseNotification = configuration
                .GetSection("FirebaseNotification")
                .Get<FirebaseNotification>();

            services.AddScoped<IPushNotificationSender, PushNotificationSender>(p =>
                new PushNotificationSender(firebaseNotification));

            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddScoped<IEmailSender, EmailSender.Implementation.EmailSender>(e =>
                new EmailSender.Implementation.EmailSender(emailConfig));

            services.AddScoped<INotificationService, NotificationService>();

            services.AddSingleton<IQuartzHostedService, QuartzHostedService>();
            services.AddHostedService(sp => sp.GetRequiredService<IQuartzHostedService>());
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>(s =>
                new StdSchedulerFactory(new NameValueCollection()));
            services.AddScoped<ISchedulerDataLoader, SchedulerDataLoader>();
            services.AddScoped<ISchedulerService, SchedulerService>();

            services.AddSingleton<NotificationJob>();

            services.AddScoped<IExportService, ExportService>();
        }
    }
}