namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a department that certain elements, such as rooms and teachers, can be associated with.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// The primary key of this item in the WebUntis database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the department.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full name of the department.
        /// </summary>
        public string LongName { get; set; }
    }
}
