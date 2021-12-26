﻿// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a timeslot on a timegrid.
    /// </summary>
    public class TimeUnit
    {
        /// <summary>
        /// A short descriptive name for the time unit. For instance, Period 1.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start time in 24-hour notation. For instance, 930.
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// The end time in 24-hour notation. For instance, 1015.
        /// </summary>
        public int EndTime { get; set; }

        /// <inheritdoc/>
        public override string ToString() => string.Format("{0}-{1}", StartTime.ToString("00:00"), EndTime.ToString("00:00"));
    }
}
