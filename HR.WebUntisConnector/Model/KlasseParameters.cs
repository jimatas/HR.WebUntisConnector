// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// The parameters to the overloaded GetKlassen method.
    /// </summary>
    public class KlasseParameters
    {
        /// <summary>
        /// The ID of the school year for which the klassen are to be retrieved.
        /// </summary>
        [JsonPropertyName("schoolyearId")]
        public int SchoolYearId { get; set; }
    }
}
