using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalendar.DAL.EF.Context;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.DAL.Repositories.Implementation;
using Xunit;

namespace WebCalendar.UnitTests.DALTests
{
    public class CRUDRequestsTests
    {
        [Fact]
        public async void GetAllTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "Database")
           .Options;

            List<Calendar> calendarsSeed = new List<Calendar>
            {
                new Calendar { Title = "calendar 1" },
                new Calendar { Title = "calendar 2" },
                new Calendar { Title = "calendar 3" }
            };
            // Insert seed data into the database using one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                context.Calendars.AddRange(calendarsSeed);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                EFRepositoryAsync<Calendar> calendarRepository =
                    new EFRepositoryAsync<Calendar>(context);
                List<Calendar> calendars = (await calendarRepository.GetAllAsync()).ToList();

                Assert.Equal(calendarsSeed.Count, calendars.Count);
            }
        }

         [Fact]
        public async void AdditionTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "Database")
           .Options;


            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                EFRepositoryAsync<Calendar> calendarRepository =
                    new EFRepositoryAsync<Calendar>(context);

                List<Calendar> calendars = context.Calendars.ToList();

                await calendarRepository.AddAsync(new Calendar { Title = "calendar 4" });
                await calendarRepository.SaveAsync();

                List<Calendar> updatedCalendars = context.Calendars.ToList();

                int expected = calendars.Count + 1;
                int actual = updatedCalendars.Count;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async void DeletionTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "Database")
           .Options;

            const string CALENDAR_NAME = "calendar to delete";

            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                EFRepositoryAsync<Calendar> calendarRepository =
                    new EFRepositoryAsync<Calendar>(context);

                await calendarRepository.AddAsync(new Calendar { Title = CALENDAR_NAME });
                await calendarRepository.SaveAsync();

                List<Calendar> calendars = context.Calendars.ToList();

                Calendar calendarToDelete = calendars.First(c => c.Title == CALENDAR_NAME);

                calendarRepository.Remove(calendarToDelete);
                await calendarRepository.SaveAsync();

                List<Calendar> updatedCalendars = context.Calendars.ToList();

                int actual = updatedCalendars.Count();
                int expected = calendars.Count() - 1;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async void UpdationTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "Database")
           .Options;

            const string CALENDAR_TO_UPDATE_NAME = "calendar to update";
            const string UPDATED_CALENDAR_NAME = "updated calendar";

            using (var context = new ApplicationDbContext(options))
            {
                EFRepositoryAsync<Calendar> calendarRepository =
                    new EFRepositoryAsync<Calendar>(context);

                await calendarRepository.AddAsync(new Calendar { Title = CALENDAR_TO_UPDATE_NAME });
                await calendarRepository.SaveAsync();

                List<Calendar> calendars = context.Calendars.ToList();

                Calendar calendarToUpdate = calendars
                    .First(c => c.Title == CALENDAR_TO_UPDATE_NAME);
                calendarToUpdate.Title = UPDATED_CALENDAR_NAME;
                HashSet<Calendar> expectedCalendars = calendars.ToHashSet();

                calendarRepository.Update(calendarToUpdate);
                await calendarRepository.SaveAsync();

                HashSet<Calendar> actualCalendars = context.Calendars.ToHashSet();

                Assert.Equal(expectedCalendars, actualCalendars);
            }
        }
    }
}
