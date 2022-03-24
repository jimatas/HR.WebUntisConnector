using System;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// A simple structure to represent date/time ranges with.
    /// </summary>
    public struct DateTimeRange : IEquatable<DateTimeRange>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="start">The starting point of the date/time range.</param>
        /// <param name="end">The ending point of the date/time range.</param>
        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start <= end ? start : end;
            End = end > start ? end : start;
        }

        /// <summary>
        /// The starting point of the date/time range.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// The ending point of the date/time range.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// A <see cref="TimeSpan"/> that represents the duration of the date/time range.
        /// </summary>
        public TimeSpan Duration => End - Start;

        /// <summary>
        /// Determines whether a specified point in time falls in this date/time range.
        /// </summary>
        /// <param name="value">The date/time to check for inclusion.</param>
        /// <returns></returns>
        public bool Includes(DateTime value) => Start <= value && End >= value;

        /// <summary>
        /// Determines whether a specified date/time range falls completely in this date/time range.
        /// </summary>
        /// <param name="other">The date/time range to check for inclusion.</param>
        /// <returns></returns>
        public bool Includes(DateTimeRange other) => Start <= other.Start && End >= other.End;

        /// <summary>
        /// Determines whether a specified date/time range falls either completely or partially in this date/time range.
        /// </summary>
        /// <param name="other">The date/time range to check for overlap.</param>
        /// <returns></returns>
        public bool Overlaps(DateTimeRange other) => Start <= other.End && End >= other.Start;

        #region System.Object overrides
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is DateTimeRange other && Equals(other);
        /// <inheritdoc/>
        public bool Equals(DateTimeRange other) => Start == other.Start && End == other.End;
        /// <inheritdoc/>
        public override int GetHashCode() => (Start, End).GetHashCode();
        /// <inheritdoc/>
        public override string ToString() => $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        #endregion

        #region Operator overloads
        public static bool operator ==(DateTimeRange x, DateTimeRange y) => x.Equals(y);
        public static bool operator !=(DateTimeRange x, DateTimeRange y) => !(x == y);
        #endregion
    }
}
