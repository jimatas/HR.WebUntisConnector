namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Encapsulates the parameters to the <c>getTimetable</c> method overload that returns a comprehensive timetable.
    /// </summary>
    public class ComprehensiveTimetableParameters
    {
        /// <summary>
        /// The various options for the timetable. This is a required property.
        /// </summary>
        public ComprehensiveTimetableOptions Options { get; set; }
    }
}
