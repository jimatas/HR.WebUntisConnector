// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Represents a subject or course.
    /// </summary>
    public class Subject : Element
    {
        /// <inheritdoc/>
        public override ElementType Type => ElementType.Subject;

        /// <summary>
        /// An alternate name for the subject.
        /// </summary>
        public string AlternateName { get; set; }
    }
}
