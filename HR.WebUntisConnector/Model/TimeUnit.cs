namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a timeslot on the timegrid.
    /// </summary>
    public class TimeUnit
    {
        /// <summary>
        /// A short descriptive name for the time unit. For instance, Period 1.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start time in 24-hour notation. For instance, 930.
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// The end time in 24-hour notation. For instance, 1015.
        /// </summary>
        public int EndTime { get; set; }

        /// <inheritdoc/>
        public override string ToString() => string.Format("{0}-{1}", StartTime.ToString("00:00"), EndTime.ToString("00:00"));
    }
}
