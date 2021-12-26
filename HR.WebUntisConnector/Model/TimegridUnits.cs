// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Collections.Generic;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents the timegrid for a particular day.
    /// </summary>
    public class TimegridUnits
    {
        /// <summary>
        /// The day of the week where Sunday is 1, Monday is 2, Tuesday is 3, Wednesday is 4, Thursday is 5, Friday is 6 and Saturday is 7.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// The individual timeslots.
        /// </summary>
        public IEnumerable<TimeUnit> TimeUnits { get; set; }
    }
}
