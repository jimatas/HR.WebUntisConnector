// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a class, that is, a body of students.
    /// The WebUntis API uses the German name for class, which is klasse, so as to avoid having to escape what is considered a keyword in many programming languages.
    /// </summary>
    public class Klasse : Element
    {
        /// <inheritdoc/>
        public override ElementType Type => ElementType.Klasse;

        /// <summary>
        /// The department this klasse is associated with.
        /// </summary>
        [JsonIgnore]
        public Department Department { get; set; }

        /// <summary>
        /// Not meant to be used directly by external code.
        /// Use the Id property of <see cref="Department"/> instead.
        /// </summary>
        [JsonPropertyName("did")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? DepartmentId
        {
            get => Department?.Id;
            set
            {
                if (value != null)
                {
                    if (Department is null)
                    {
                        Department = new Department();
                    }

                    Department.Id = (int)value;
                }
            }
        }

        /// <summary>
        /// The first teacher, out of a possible two, that has been assigned to this klasse.
        /// </summary>
        [JsonIgnore]
        public Teacher Teacher1 { get; set; }

        /// <summary>
        /// Not meant to be used directly by external code.
        /// Use the Id property of <see cref="Teacher1"/> instead.
        /// </summary>
        [JsonPropertyName("teacher1")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? Teacher1Id
        {
            get => Teacher1?.Id;
            set
            {
                if (value != null)
                {
                    if (Teacher1 is null)
                    {
                        Teacher1 = new Teacher();
                    }

                    Teacher1.Id = (int)value;
                }
            }
        }

        /// <summary>
        /// The second teacher that has been assigned to this klasse.
        /// </summary>
        [JsonIgnore]
        public Teacher Teacher2 { get; set; }

        /// <summary>
        /// Not meant to be used directly by external code.
        /// Use the Id property of <see cref="Teacher2"/> instead.
        /// </summary>
        [JsonPropertyName("teacher2")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? Teacher2Id
        {
            get => Teacher2?.Id;
            set
            {
                if (value != null)
                {
                    if (Teacher2 is null)
                    {
                        Teacher2 = new Teacher();
                    }

                    Teacher2.Id = (int)value;
                }
            }
        }
    }
}
