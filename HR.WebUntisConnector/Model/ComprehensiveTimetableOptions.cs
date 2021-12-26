// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// The options that indicate which data are to be returned in the timetable.
    /// </summary>
    public class ComprehensiveTimetableOptions
    {
        /// <summary>
        /// The element for which to retrieve the timetable. 
        /// </summary>
        public ComprehensiveTimetableElement Element { get; set; }

        /// <summary>
        /// The start date in ISO-8601 format. For instance, 20190902.
        /// Default value is today's date.
        /// </summary>
        public int StartDate { get; set; } = int.Parse(DateTime.Today.ToString("yyyyMMdd"));

        /// <summary>
        /// The end date in ISO-8601 format. For instance, 20190906.
        /// Default value is today's date.
        /// </summary>
        public int EndDate { get; set; } = int.Parse(DateTime.Today.ToString("yyyyMMdd"));

        /// <summary>
        /// Indicates whether to show only the base timetable, that is, without booking information.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("onlyBaseTimetable")]
        public bool ShowOnlyBaseTimetable { get; set; }

        /// <summary>
        /// Indicates whether to show information about the booking.
        /// Default value is <c>false</c>.
        /// </summary>
        public bool ShowBooking { get; set; }

        /// <summary>
        /// Indicates whether to show the informational text that was entered for the lesson.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("showInfo")]
        public bool ShowInformation { get; set; }

        /// <summary>
        /// Indicates whether to show information pertaining to a possible substitution.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("showSubstText")]
        public bool ShowSubstitutionText { get; set; }

        /// <summary>
        /// Indicates whether to show the lesson text.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("showLsText")]
        public bool ShowLessonText { get; set; }

        /// <summary>
        /// Indicates whether to show the lesson number.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("showLsNumber")]
        public bool ShowLessonNumber { get; set; }

        /// <summary>
        /// Indicates whether to show the name of the student group.
        /// Default value is <c>false</c>.
        /// </summary>
        [JsonPropertyName("showStudentgroup")]
        public bool ShowStudentGroup { get; set; }

        /// <summary>
        /// The individual fields to return in the <see cref="Timetable.Klassen"/> property.
        /// The possible values are <c>id</c>, <c>name</c>, <c>longname</c>, and <c>externalkey</c>.
        /// Use the constants defined in the <see cref="ElementFields"/> class.
        /// </summary>
        public IEnumerable<string> KlasseFields { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// The individual fields to return in the <see cref="Timetable.Rooms"/> property.
        /// The possible values are <c>id</c>, <c>name</c>, <c>longname</c>, and <c>externalkey</c>.
        /// Use the constants defined in the <see cref="ElementFields"/> class.
        /// </summary>
        public IEnumerable<string> RoomFields { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// The individual fields to return in the <see cref="Timetable.Subjects"/> property.
        /// The possible values are <c>id</c>, <c>name</c>, <c>longname</c>, and <c>externalkey</c>.
        /// Use the constants defined in the <see cref="ElementFields"/> class.
        /// </summary>
        public IEnumerable<string> SubjectFields { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// The individual fields to return in the <see cref="Timetable.Teachers"/> property.
        /// The possible values are <c>id</c>, <c>name</c>, <c>longname</c>, and <c>externalkey</c>.
        /// Use the constants defined in the <see cref="ElementFields"/> class.
        /// </summary>
        public IEnumerable<string> TeacherFields { get; set; } = Enumerable.Empty<string>();
    }
}
