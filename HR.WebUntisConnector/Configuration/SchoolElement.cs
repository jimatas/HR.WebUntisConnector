// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Collections.Generic;
using System.Xml.Serialization;

namespace HR.WebUntisConnector.Configuration
{
    /// <summary>
    /// Represents a &lt;school&gt; element in the WebUntis configuration section.
    /// </summary>
    public class SchoolElement
    {
        /// <summary>
        /// The WebUntis school name.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The institutes that map to this WebUntis school.
        /// Exposed as a generic list of <see cref="InstituteElement"/>, as <see cref="XmlSerializer"/> does not support serializing interfaces.
        /// </summary>
        [XmlElement(ElementName = "institute", IsNullable = false)]
        public List<InstituteElement> Institutes { get; set; } = new List<InstituteElement>();

        /// <summary>
        /// A possible URL override for this particular school.
        /// </summary>
        [XmlAttribute(AttributeName = "serviceUrl")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// A possible username override for this particular school.
        /// </summary>
        [XmlAttribute(AttributeName = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// A possible password override for this particular school.
        /// </summary>
        [XmlAttribute(AttributeName = "password")]
        public string Password { get; set; }
    }
}
