using System.ComponentModel.DataAnnotations;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Enumerates the various WebUntis element types.
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// The element is a class.
        /// </summary>
        [Display(Name = "Class")]
        Klasse = 1,

        /// <summary>
        /// The element is a teacher.
        /// </summary>
        Teacher = 2,

        /// <summary>
        /// The element is a subject.
        /// </summary>
        Subject = 3,

        /// <summary>
        /// The element is a room.
        /// </summary>
        Room = 4,

        /// <summary>
        /// The element is a student.
        /// </summary>
        Student = 5
    }
}
