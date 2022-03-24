using HR.WebUntisConnector.Model;

using System.Collections.Generic;
using System.Linq;

namespace HR.WebUntisConnector.Extensions
{
    /// <summary>
    /// Extension methods on the <see cref="Timetable"/> class.
    /// </summary>
    public static class TimetableExtensions
    {
        /// <summary>
        /// Groups the individual items in a collection of <see cref="Timetable"/> objects by their <c>Date</c> and <c>LessonNumber</c> properties and returns the result as a collection of <see cref="TimetableGroup"/> objects.
        /// </summary>
        /// <param name="timetables">The timetables to group.</param>
        /// <param name="timegrids">Optionally specified timegrid definitions by which to determine whether two subsequent timetables are considered adjacent to one and other.</param>
        /// <returns></returns>
        public static IEnumerable<TimetableGroup> ToTimetableGroups(this IEnumerable<Timetable> timetables, IEnumerable<TimegridUnits> timegrids = null)
        {
            ICollection<TimetableGroup> timetableGroups = new List<TimetableGroup>();
            ICollection<Timetable> relatedTimetables = new List<Timetable>();

            foreach (var groupedTimetables in timetables.GroupBy(table => table.Date).Select(group => group.AsEnumerable()))
            {
                foreach (var lessonNumber in groupedTimetables.Select(table => table.LessonNumber / 100).Distinct())
                {
                    foreach (var timetable in groupedTimetables.Where(table => table.LessonNumber / 100 == lessonNumber).OrderBy(table => table.StartTime))
                    {
                        if (relatedTimetables.Any() && !AreTimetablesAdjacent(relatedTimetables.Last(), timetable))
                        {
                            GroupAndClearRelatedTimetables();
                        }

                        relatedTimetables.Add(timetable);
                    }

                    if (relatedTimetables.Any())
                    {
                        GroupAndClearRelatedTimetables();
                    }
                }
            }

            return timetableGroups.OrderBy(group => group.Date).ThenBy(group => group.StartTime).ToArray();

            bool AreTimetablesAdjacent(Timetable first, Timetable second)
            {
                if (first.EndTime >= second.StartTime)
                {
                    return true;
                }

                if (timegrids != null && timegrids.Any())
                {
                    var timegrid = timegrids.SingleOrDefault(grid => grid.GetDayOfWeek() == first.GetStartDateTime().DayOfWeek);
                    if (timegrid != null)
                    {
                        var firstTimeslotLocated = false;
                        foreach (var timeslot in timegrid.TimeUnits.OrderBy(unit => unit.StartTime))
                        {
                            if (firstTimeslotLocated)
                            {
                                if (second.StartTime == timeslot.StartTime && second.EndTime == timeslot.EndTime)
                                {
                                    return true;
                                }

                                firstTimeslotLocated = false;
                            }

                            if (first.StartTime == timeslot.StartTime && first.EndTime == timeslot.EndTime)
                            {
                                firstTimeslotLocated = true;
                            }
                        }
                    }
                }
                return false;
            }

            void GroupAndClearRelatedTimetables()
            {
                timetableGroups.Add(new TimetableGroup()
                {
                    Date = relatedTimetables.First().Date,
                    StartTime = relatedTimetables.First().StartTime,
                    EndTime = relatedTimetables.Last().EndTime,
                    Timetables = relatedTimetables.ToArray(),
                    LessonNumbers = relatedTimetables.Select(table => table.LessonNumber).Distinct().Cast<int>().ToArray()
                });
                relatedTimetables.Clear();
            }
        }
    }
}
