// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Defines constants for the various types of keys.
    /// </summary>
    public static class KeyTypes
    {
        /// <summary>
        /// The key is the primary key of the item in the WebUntis database.
        /// </summary>
        public const string Id = "id";

        /// <summary>
        /// The key is the unique name of the item.
        /// </summary>
        public const string Name = "name";

        /// <summary>
        /// The key is the identifier of the item in the source system.
        /// </summary>
        public const string ExternalKey = "externalkey";
    }
}
