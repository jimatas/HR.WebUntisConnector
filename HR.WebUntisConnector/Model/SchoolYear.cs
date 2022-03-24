using System.Collections.Generic;
using System.Linq;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a complete school year.
    /// </summary>
    public class SchoolYear
    {
        /// <summary>
        /// The primary key of this item in the WebUntis database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the school year. For instance, 2019-2020.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start date of the school year. For instance, 20190902.
        /// </summary>
        public int StartDate { get; set; }

        /// <summary>
        /// The end date of the school year. For instance, 20200703.
        /// </summary>
        public int EndDate { get; set; }

        /// <summary>
        /// The semesters that have been defined in this school year, if any.
        /// </summary>
        public IEnumerable<Semester> Semesters { get; set; } = Enumerable.Empty<Semester>();
    }
}
