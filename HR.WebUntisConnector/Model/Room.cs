// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a classroom.
    /// </summary>
    public class Room : Element
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public override ElementType Type => ElementType.Room;

        /// <summary>
        /// The department that this room is optionally associated with.
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
        /// The name of the building this room is located in.
        /// </summary>
        [JsonPropertyName("building")]
        public string BuildingName { get; set; }
    }
}
