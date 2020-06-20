using System;
using System.Collections.Generic;
using System.Linq;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL.EF.Context;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;

namespace WebCalendar.DAL.EF.Initializer
{
    public class EFDataInitializer : IDataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EFDataInitializer(ApplicationDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }
        
        public void Seed()
        {
            if (_context.Database.EnsureDeleted())
            {
                _context.Database.EnsureCreated();
                AddUser();
                AddCalendar();
                AddCalendarUser();
                AddEvent();
                AddUserEvent();
            }
        }

        private void AddUser()
        {
            var users = new List<User>()
            {
                new User
                {
                    Id = new Guid(),
                    FirstName = "Mykhail",
                    LastName = "Yermolenko",
                    Email = "ermolenko1999@gmail.com",
                    Admin = false,
                    IsSubscribedToEmailNotifications = true,
                },
                
                new User
                {
                    Id = new Guid(),
                    FirstName = "Michael",
                    LastName = "Yermolenko",
                    Email = "mixaluch_ermolenko@mail.ru",
                    Admin = true,
                    IsSubscribedToEmailNotifications = false,
                }
            };

            foreach (User user in users)
            {
                UserRegisterServiceModel userRegister = _mapper.Map<User, UserRegisterServiceModel>(user);
                userRegister.Password = userRegister.Email;
                _userService.RegisterAsync(userRegister).Wait();
            }
        }

        private void AddCalendar()
        {
            var userid = _context.Users.ToArray();

            var calendars = new List<Calendar>()
            {
                new Calendar
                {
                    Id = new Guid(),
                    Title = "КС-16-1",
                    Description = "Комп'ютерні науки та інформаційні технології",
                    UserId = userid[0].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Title = "КИ-16-1",
                    Description = "Комп'ютерна інженерія",
                    UserId = userid[1].Id
                }
            };

            _context.Calendars.AddRange(calendars);
            _context.SaveChanges();
        }
        private void AddCalendarUser()
        {
            var calendarid = _context.Calendars.ToArray();
            var userid = _context.Users.ToArray();

            var calendaruser = new List<CalendarUser>()
            {
                new CalendarUser
                {
                    CalendarId = calendarid[0].Id,
                    UserId = userid[0].Id,
                }
            };

            _context.CalendarUsers.AddRange(calendaruser);
            _context.SaveChanges();
        }

        private void AddEvent()
        {
            var calendarid = _context.Calendars.ToArray();

            var events = new List<Event>()
            {
                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event #1",
                    StartTime = new DateTime(2020, 6, 1, 0, 0, 0),
                    EndTime = new DateTime(2020, 6, 1, 23, 0, 0),
					CalendarId = calendarid[2].Id
				},

				new Event
				{
					Id = new Guid(),
					Title = "Event#2",
					Description = "Event #2",
					StartTime = new DateTime(2020, 6, 13, 0, 0, 0),
					EndTime = new DateTime(2100, 6, 13, 22, 59, 59),
					CalendarId = calendarid[0].Id
				},

				new Event
                {
                    Id = new Guid(),
                    Title = "Event#3",
                    Description = "Event #3",
                    StartTime = new DateTime(2020, 6, 24, 12, 0 , 0),
                    EndTime = new DateTime(2020, 6, 24, 13, 30, 0),
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event #4",
                    Description = "",
                    StartTime = new DateTime(2020, 6, 12, 18, 0, 0),
                    EndTime = new DateTime(2020, 6, 12, 18, 30, 0),
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event #5",
                    Description = "Event #5",
                    StartTime = new DateTime(2020, 6, 23, 10, 0, 0),
                    EndTime = new DateTime(2020, 6, 23, 17, 59, 59),
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#6",
                    Description = "Event#6",
                    StartTime = new DateTime(2020, 5, 18, 22, 0, 0),
                    EndTime = new DateTime(2020, 5, 19, 0, 0, 0),
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#7",
                    Description = "Event#7",
                    StartTime = new DateTime(2020, 6, 28, 1, 0, 0),
                    EndTime = new DateTime(2020, 6, 28, 2, 0, 0),
                    CalendarId = calendarid[0].Id
                }
            };

            _context.Events.AddRange(events);
            _context.SaveChanges();
        }

        private void AddUserEvent()
        {
            var userid = _context.Users.ToArray();
            var eventid = _context.Events.ToArray();

            var userevent = new List<UserEvent>()
            {
                //new UserEvent
                //{
                //    UserId = userid[0].Id,
                //    EventId = eventid[3].Id
                //},

                //new UserEvent
                //{
                //    UserId = userid[0].Id,
                //    EventId = eventid[1].Id
                //},

                //new UserEvent
                //{
                //    UserId = userid[0].Id,
                //    EventId = eventid[0].Id
                //},

                //new UserEvent
                //{
                //    UserId = userid[0].Id,
                //    EventId = eventid[2].Id
                //}
            };

            _context.UserEvents.AddRange(userevent);
            _context.SaveChanges();
        }

    }
}