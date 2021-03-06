using System;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Encapsulates the parameters to the <c>getTimetable</c> method.
    /// </summary>
    public class TimetableParameters
    {
        /// <summary>
        /// The ID of the element for which to retrieve the timetable.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The type of element that the <see cref="Id"/> property refers to.
        /// Use one of the constants defined in the <see cref="ElementType"/> class.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// The start date in ISO-8601 format. For instance, 20190902.
        /// Default value is today's date.
        /// </summary>
        public int StartDate { get; set; } = int.Parse(DateTime.Today.ToString("yyyyMMdd"));

        /// <summary>
        /// The end date in ISO-8601 format. For instance, 20190906.
        /// Default value is today's date.
        /// </summary>
        public int EndDate { get; set; } = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
    }
}
