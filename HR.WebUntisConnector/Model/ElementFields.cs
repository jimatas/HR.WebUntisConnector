// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Defines constants for the lower-cased field names of an element.
    /// </summary>
    public static class ElementFields
    {
        /// <summary>
        /// The field that contains the element's primary key in the WebUntis database.
        /// </summary>
        public const string Id = "id";

        /// <summary>
        /// The field that contains the name of the element.
        /// </summary>
        public const string Name = "name";

        /// <summary>
        /// The field that contains the full name of the element.
        /// </summary>
        public const string LongName = "longname";

        /// <summary>
        /// The field that contains the external key of the element.
        /// </summary>
        public const string ExternalKey = "externalkey";
    }
}
