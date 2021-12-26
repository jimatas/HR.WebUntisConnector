// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Encapsulates the parameters to the GetTimetable method overload that returns a comprehensive timetable.
    /// </summary>
    public class ComprehensiveTimetableParameters
    {
        /// <summary>
        /// The various options for the timetable. This is a required property.
        /// </summary>
        public ComprehensiveTimetableOptions Options { get; set; }
    }
}
