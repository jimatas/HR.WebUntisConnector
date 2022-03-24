using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a teacher or instructor.
    /// </summary>
    public class Teacher : Element
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public override ElementType Type => ElementType.Teacher;

        /// <summary>
        /// All the teacher's departments.
        /// </summary>
        [JsonPropertyName("dids")]
        public IEnumerable<Department> Departments { get; set; } = Enumerable.Empty<Department>();

        /// <summary>
        /// The last name of this teacher. 
        /// This is an alias for <see cref="Element.LongName"/>.
        /// </summary>
        public string LastName => LongName;

        /// <summary>
        /// The first name of the teacher, if available.
        /// </summary>
        [JsonPropertyName("foreName")]
        public string FirstName { get; set; }
    }
}
