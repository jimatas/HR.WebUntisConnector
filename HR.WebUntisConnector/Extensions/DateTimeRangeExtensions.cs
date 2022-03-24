using HR.WebUntisConnector.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebUntisConnector.Extensions
{
    /// <summary>
    /// Helper methods to convert API objects with a start and end date/time to <see cref="DateTimeRange"/> objects.
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        /// Returns a <see cref="DateTimeRange"/> object whose <c>Start</c> and <c>End</c> properties are set to this <see cref="SchoolYear"/> object's <c>StartDate</c> and <c>EndDate</c> properties.
        /// </summary>
        /// <param name="schoolYear"></param>
        /// <returns></returns>
        public static DateTimeRange ToDateTimeRange(this SchoolYear schoolYear)
            => new DateTimeRange(schoolYear.GetStartDateTime(), schoolYear.GetEndDateTime());

        /// <summary>
        /// Returns a <see cref="DateTimeRange"/> object whose <c>Start</c> and <c>End</c> properties are set to this <see cref="Holiday"/> object's <c>StartDate</c> and <c>EndDate</c> properties.
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public static DateTimeRange ToDateTimeRange(this Holiday holiday)
            => new DateTimeRange(holiday.GetStartDateTime(), holiday.GetEndDateTime());

        /// <summary>
        /// Converts a sequence of <see cref="SchoolYear"/> objects to a sequence of <see cref="DateTimeRange"/> objects that fall between the specified start and end dates.
        /// </summary>
        /// <param name="schoolYears"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTimeRange> ToDateTimeRanges(this IEnumerable<SchoolYear> schoolYears, DateTime startDate, DateTime endDate)
        {
            IList<DateTimeRange> dateTimeRanges = new List<DateTimeRange>();

            foreach (var schoolYear in schoolYears.OrderBy(y => y.StartDate))
            {
                if (schoolYear.ToDateTimeRange().Overlaps(new DateTimeRange(startDate, endDate)))
                {
                    dateTimeRanges.Add(new DateTimeRange(
                        Max(schoolYear.GetStartDateTime(), startDate),
                        Min(schoolYear.GetEndDateTime(), endDate)));
                }
            }

            return dateTimeRanges;

            DateTime Min(DateTime x, DateTime y) => x <= y ? x : y;
            DateTime Max(DateTime x, DateTime y) => x >= y ? x : y;
        }
    }
}
