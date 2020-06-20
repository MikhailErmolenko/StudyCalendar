using System;
using System.Collections.Generic;
using System.Linq;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Scheduler
{
    public static class CronExpressionGenerator
    {
        private const char SEPARATOR = ',';
        private const string UNDEFINED = "?";

        public static readonly string TriggerInThePast = Guid.NewGuid().ToString(); 

        public static string GetCronExpression(this IRepeatableActivity schedule)
        {
            string seconds = GetSeconds(schedule);
            string minutes = GetMinutes(schedule);
            string hours = GetHours(schedule);
            string daysOfMonth = GetDaysOfMonth(schedule);
            string monthes = GetMonths(schedule);
            string daysOfWeek = GetDaysOfWeek(schedule);
            string years = GetYears(schedule);

            if (monthes == "" || years == "")
            {
                if (schedule.StartTime <= DateTime.Now)
                {
                    return TriggerInThePast;
                }

                return $"{seconds} {minutes} {hours} {schedule.StartTime.Day} {schedule.StartTime.Month} {UNDEFINED} {schedule.StartTime.Year}";
            }

            return $"{seconds} {minutes} {hours} {daysOfMonth} {monthes} {daysOfWeek} {years}";
        }

        private static string Separate(IEnumerable<int> items)
        {
            IEnumerable<string> stringsOfDays = items.Select(d => d.ToString());

            return String.Join(SEPARATOR, stringsOfDays);
        }

        private static string GetSeconds(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Second.ToString();
        }

        private static string GetMinutes(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Minute.ToString();
        }

        private static string GetHours(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Hour.ToString();
        }

        private static string GetDaysOfMonth(IRepeatableActivity schedule)
        {
            IEnumerable<int> daysOfMonth = schedule.DaysOfMounth;

            if (daysOfMonth == null || !daysOfMonth.Any())
            {
                return UNDEFINED;
            }

            return Separate(daysOfMonth);
        }

        private static string GetMonths(IRepeatableActivity schedule)
        {
            return Separate(schedule.Monthes);
        }

        private static string GetDaysOfWeek(IRepeatableActivity schedule)
        {
            IEnumerable<int> daysOfWeek = schedule.DaysOfWeek;

            if (daysOfWeek == null || !daysOfWeek.Any())
            {
                return UNDEFINED;
            }

            return Separate(daysOfWeek);
        }

        private static string GetYears(IRepeatableActivity schedule)
        {
            return Separate(schedule.Years);
        }
    }
}
