namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// A semester in the school year.
    /// </summary>
    public class Semester
    {
        /// <summary>
        /// The primary key of this item in the WebUntis database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the semester. For instance, OP1 &amp; OP2.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start date of the semester. For instance, 20190902.
        /// </summary>
        public int StartDate { get; set; }

        /// <summary>
        /// The end date of the semester. For instance, 20200131.
        /// </summary>
        public int EndDate { get; set; }
    }
}
