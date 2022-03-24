using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a timetable for a particular element, such as a class or subject.
    /// </summary>
    public class Timetable
    {
        /// <summary>
        /// The primary key of this item in the WebUntis database.
        /// </summary>
        public int Id { get; set; }

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
        /// The klassen that are linked to this timetable.
        /// </summary>
        [JsonPropertyName("kl")]
        public IEnumerable<Klasse> Klassen { get; set; } = Enumerable.Empty<Klasse>();

        /// <summary>
        /// The teachers that are linked to this timetable.
        /// </summary>
        [JsonPropertyName("te")]
        public IEnumerable<Teacher> Teachers { get; set; } = Enumerable.Empty<Teacher>();

        /// <summary>
        /// The subjects that are linked to this timetable.
        /// </summary>
        [JsonPropertyName("su")]
        public IEnumerable<Subject> Subjects { get; set; } = Enumerable.Empty<Subject>();

        /// <summary>
        /// The rooms that are linked to this timetable.
        /// </summary>
        [JsonPropertyName("ro")]
        public IEnumerable<Room> Rooms { get; set; } = Enumerable.Empty<Room>();

        /// <summary>
        /// The name of the student group, if available.
        /// </summary>
        [JsonPropertyName("sg")]
        public string StudentGroup { get; set; }

        /// <summary>
        /// Informational text that was entered for this lesson, if any.
        /// </summary>
        [JsonPropertyName("info")]
        public string Information { get; set; }

        /// <summary>
        /// Indicates whether this lesson has been cancelled or is in some other way irregular, such as through an unplanned subsitution.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A short code that identifies the lesson type, where <c>oh</c> stands for office hour, <c>sb</c> for standby, <c>bs</c> for break supervision and <c>ex</c> for examination.
        /// </summary>
        [JsonPropertyName("lstype")]
        public string LessonType { get; set; }

        /// <summary>
        /// The lesson number, if available.
        /// </summary>
        [JsonPropertyName("lsnumber")]
        public int? LessonNumber { get; set; }

        /// <summary>
        /// The lesson text, if available. Merged if texts differ per period.
        /// </summary>
        [JsonPropertyName("lstext")]
        public string LessonText { get; set; }

        /// <summary>
        /// Substitution text.
        /// </summary>
        [JsonPropertyName("substText")]
        public string SubstitutionText { get; set; }

        /// <summary>
        /// Remark pertaining to the booking, if any.
        /// </summary>
        [JsonPropertyName("bkRemark")]
        public string BookingRemark { get; set; }

        /// <summary>
        /// Additional remark (field 2) pertaining to the booking, if any.
        /// </summary>
        [JsonPropertyName("bkText")]
        public string BookingText { get; set; }

        /// <summary>
        /// The statistical flags, if available.
        /// </summary>
        [JsonPropertyName("statflags")]
        public string StatisticalFlags { get; set; }

        /// <summary>
        /// Describes the sort of activity.
        /// </summary>
        public string ActivityType { get; set; }
    }
}
