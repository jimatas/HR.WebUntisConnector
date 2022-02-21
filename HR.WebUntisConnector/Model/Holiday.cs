// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a holiday in the timetable.
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// The primary key of the item in the WebUntis database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the holiday. For instance, Fall-19.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full name of the holiday. For instance, Fall vacation 2019.
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// The start date of the holiday. For instance, 20191021.
        /// </summary>
        public int StartDate { get; set; }

        /// <summary>
        /// The end date of the holiday. For instance, 20191025.
        /// </summary>
        public int EndDate { get; set; }
    }
}
