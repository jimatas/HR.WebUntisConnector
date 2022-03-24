using HR.WebUntisConnector.Model;

using System;
using System.Globalization;

namespace HR.WebUntisConnector.Extensions
{
    /// <summary>
    /// Helpful extensions for working with the dates and times used in the WebUntis API.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the date and start time of the timetable.
        /// </summary>
        /// <param name="timetable"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this Timetable timetable)
            => DateTime.ParseExact(timetable.Date.ToString() + timetable.StartTime.ToString("0000"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the date and end time of the timetable.
        /// </summary>
        /// <param name="timetable"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this Timetable timetable)
            => DateTime.ParseExact(timetable.Date.ToString() + timetable.EndTime.ToString("0000"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the date and start time of the first timetable in the timetable group.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this TimetableGroup group)
            => DateTime.ParseExact(group.Date.ToString() + group.StartTime.ToString("0000"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the date and end time of the last timetable in the timetable group.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this TimetableGroup group)
            => DateTime.ParseExact(group.Date.ToString() + group.EndTime.ToString("0000"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the start date of the school year.
        /// </summary>
        /// <param name="schoolYear"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this SchoolYear schoolYear)
            => DateTime.ParseExact(schoolYear.StartDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the end date of the school year.
        /// </summary>
        /// <param name="schoolYear"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this SchoolYear schoolYear)
            => DateTime.ParseExact(schoolYear.EndDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the start date of the semester.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this Semester semester)
            => DateTime.ParseExact(semester.StartDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the end date of the semester.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this Semester semester)
            => DateTime.ParseExact(semester.EndDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the start date of the holiday.
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this Holiday holiday)
            => DateTime.ParseExact(holiday.StartDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DateTime"/> instance that represents the end date of the holiday.
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this Holiday holiday)
            => DateTime.ParseExact(holiday.EndDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns a <see cref="DayOfWeek"/> enumeration value that represents the day number of a timegrid column.
        /// </summary>
        /// <param name="timegrid"></param>
        /// <returns></returns>
        public static DayOfWeek GetDayOfWeek(this TimegridUnits timegrid) => (DayOfWeek)(timegrid.Day - 1);

        /// <summary>
        /// Returns the ISO-8601 week number of the calendar week the <see cref="DateTime"/> instance falls in.
        /// </summary>
        /// <remarks>
        /// Note: This method returns 1 for 2012-12-31, which is correct according to ISO-8601.
        /// </remarks>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(this DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns the first weekday of the week that the <see cref="DateTime"/> instance falls in.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstWeekday(this DateTime date) => date.AddDays(-1 * ((int)date.DayOfWeek - 1));

        /// <summary>
        /// Returns the last weekday of the week that the <see cref="DateTime"/> instance falls in.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastWeekday(this DateTime date) => date.GetFirstWeekday().AddDays(DayOfWeek.Friday - DayOfWeek.Monday);

        /// <summary>
        /// Returns the first weekday given a specific week number and year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        /// <seealso href="https://stackoverflow.com/a/9064954">Calculate date from week number</seealso>
        public static DateTime GetFirstWeekdayOfWeek(int year, int weekOfYear)
        {
            var januaryFirst = new DateTime(year, 1, 1);
            var firstThursdayInJanuary = januaryFirst.AddDays(DayOfWeek.Thursday - januaryFirst.DayOfWeek);
            var firstWeekOfYear = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstThursdayInJanuary, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeekOfYear == 1)
            {
                weekOfYear -= 1;
            }

            return firstThursdayInJanuary.AddDays(weekOfYear * 7).AddDays(-3);
        }
    }
}
