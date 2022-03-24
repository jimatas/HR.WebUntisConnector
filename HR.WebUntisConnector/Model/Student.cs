using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents an individual student.
    /// </summary>
    public class Student : Element
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public override ElementType Type => ElementType.Student;

        /// <summary>
        /// The last name of this student.
        /// This is an alias for <see cref="Element.LongName"/>.
        /// </summary>
        [JsonIgnore]
        public string LastName => LongName;

        /// <summary>
        /// The first name of the student, if available.
        /// </summary>
        [JsonPropertyName("foreName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The 'natural key' through which the student can be uniquely identified, such as their student number.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// A string describing the gender of the student. 
        /// Either <c>male</c> or <c>female</c>.
        /// </summary>
        public string Gender { get; set; }
    }
}
