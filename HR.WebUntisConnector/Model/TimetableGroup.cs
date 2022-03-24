using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// A grouping of related timetables. 
    /// Timetables will be grouped together when they occur on the same date and have the same (base) lesson number.
    /// </summary>
    public class TimetableGroup
    {
        /// <summary>
        /// The individual timetables in this group.
        /// </summary>
        public IEnumerable<Timetable> Timetables { get; set; } = Enumerable.Empty<Timetable>();

        /// <summary>
        /// The date of this timetable. For instance, 20190902.
        /// </summary>
        public int Date { get; set; }

        /// <summary>
        /// The start time in 24-hour notation. For instance, 930.
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// The end time in 24-hour notation. For instance, 1015.
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        /// The distinct set of lesson numbers of the individual timetables in this timetable group.
        /// </summary>
        [JsonPropertyName("lsnumbers")]
        public IEnumerable<int> LessonNumbers { get; set; } = Enumerable.Empty<int>();
    }
}
