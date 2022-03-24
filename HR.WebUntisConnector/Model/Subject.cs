using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a subject or course.
    /// </summary>
    public class Subject : Element
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public override ElementType Type => ElementType.Subject;

        /// <summary>
        /// An alternate name for the subject.
        /// </summary>
        public string AlternateName { get; set; }
    }
}
