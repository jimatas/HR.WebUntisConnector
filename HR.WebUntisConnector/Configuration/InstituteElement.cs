// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Xml.Serialization;

namespace HR.WebUntisConnector.Configuration
{
    /// <summary>
    /// Represents an &lt;institute&gt; element in the WebUntis configuration section.
    /// </summary>
    public class InstituteElement
    {
        /// <summary>
        /// The three-letter code identifying the institute.
        /// </summary>
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// The unique name of the institute.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// An optional display name to associate with the institute.
        /// This value will be shown in drop-down lists etc.
        /// </summary>
        [XmlAttribute(AttributeName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Indicates whether to show this entry in the drop-down list. 
        /// Defaults to <c>true</c>.
        /// </summary>
        [XmlAttribute(AttributeName = "visible")]
        public bool IsVisible { get; set; } = true;
    }
}
