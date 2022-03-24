using System.Collections.Generic;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents the timegrid for a particular day.
    /// </summary>
    public class TimegridUnits
    {
        /// <summary>
        /// The day of the week where Sunday is 1, Monday is 2, Tuesday is 3, Wednesday is 4, Thursday is 5, Friday is 6 and Saturday is 7.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// The individual timeslots.
        /// </summary>
        public IEnumerable<TimeUnit> TimeUnits { get; set; }
    }
}
