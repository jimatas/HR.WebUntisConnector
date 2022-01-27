// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Specifies the element to retrieve the timetable for.
    /// </summary>
    public class ComprehensiveTimetableElement
    {
        /// <summary>
        /// The identifier (see <see cref="KeyType"/>) of the element for which to retrieve the timetable.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The type of element that the <see cref="Id"/> property refers to.
        /// </summary>
        public ElementType Type { get; set; }

        /// <summary>
        /// Denotes the type of key that is contained in the <see cref="Id"/> property.
        /// The possible values are <c>id</c> for the primary key in the WebUntis database, <c>name</c> for the element's abbreviated name, or <c>externalkey</c> for its external identifier.
        /// Use one of the constants defined in the <see cref="KeyTypes"/> class.
        /// Default value is <c>id</c>.
        /// </summary>
        public string KeyType { get; set; } = KeyTypes.Id;
    }
}
