// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Serves as a base class for the <see cref="Klasse"/>, <see cref="Student"/>, <see cref="Teacher"/>, <see cref="Room"/> and <see cref="Subject"/> classes.
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Overridden in subclasses to return an enumerated constant that identifies the element type. 
        /// </summary>
        [JsonIgnore]
        public abstract ElementType Type { get; }

        /// <summary>
        /// The primary key of this item in the WebUntis database.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// The ID of the element in the source system.
        /// </summary>
        public virtual string ExternalKey { get; set; }

        /// <summary>
        /// The name of this element.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The full name of this element.
        /// </summary>
        public virtual string LongName { get; set; }

        /// <summary>
        /// Is this element active?
        /// </summary>
        [JsonPropertyName("active")]
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// The foreground color to use for this element.
        /// </summary>
        [JsonPropertyName("foreColor")]
        public virtual string ForegroundColor { get; set; }

        /// <summary>
        /// The background color to use for this element.
        /// </summary>
        [JsonPropertyName("backColor")]
        public virtual string BackgroundColor { get; set; }

        #region System.Object overrides
        /// <inheritdoc/>
        public override string ToString() => $"{Type} with {nameof(Id)} {Id}";
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Element other && Type == other.Type && Id == other.Id;
        /// <inheritdoc/>
        public override int GetHashCode() => (Type, Id).GetHashCode();
        #endregion
    }
}
