// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace HR.WebUntisConnector.Configuration
{
    /// <summary>
    /// This class encapsulates the WebUntis configuration section as well as provide the means to access it by implementing the <see cref="IConfigurationSectionHandler"/> interface.
    /// </summary>
    [XmlRoot(ElementName = "webuntis")]
    public class WebUntisConfigurationSection : IConfigurationSectionHandler
    {
        /// <summary>
        /// A very simple implementation of <see cref="IConfigurationSectionHandler.Create(object, object, XmlNode)"/> that uses the <see cref="XmlSerializer"/> class internally.
        /// </summary>
        public object Create(object parent, object configContext, XmlNode section)
            => new XmlSerializer(typeof(WebUntisConfigurationSection)).Deserialize(new XmlNodeReader(section));

        /// <summary>
        /// Instantiates a new <see cref="WebUntisConfigurationSection"/> from the XML data contained in the specified XML file.
        /// </summary>
        /// <param name="filePathOrUri"></param>
        /// <returns></returns>
        public static WebUntisConfigurationSection FromXmlFile(string filePathOrUri)
        {
            using (var reader = XmlReader.Create(filePathOrUri))
            {
                return new XmlSerializer(typeof(WebUntisConfigurationSection)).Deserialize(reader) as WebUntisConfigurationSection;
            }
        }

        /// <summary>
        /// The URL of the JSON-RPC service to connect to.
        /// </summary>
        [XmlAttribute(AttributeName = "serviceUrl")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// The username to connect to the service with.
        /// </summary>
        [XmlAttribute(AttributeName = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// The password to connect to the service with.
        /// </summary>
        [XmlAttribute(AttributeName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// The configuration data for the WebUntis schools that can connect to the service.
        /// Exposed as a generic list of <see cref="SchoolElement"/>, as <see cref="XmlSerializer"/> does not support serializing interfaces.
        /// </summary>
        [XmlElement(ElementName = "school", IsNullable = false)]
        public List<SchoolElement> Schools { get; set; } = new List<SchoolElement>();

        [XmlAttribute(AttributeName = "cacheDuration")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CacheDurationString { get; set; }

        /// <summary>
        /// Indicates how long to cache the results that are returned by the parameterless API methods.
        /// </summary>
        public TimeSpan CacheDuration
        {
            get => TimeSpan.TryParse(CacheDurationString, out var duration) ? duration : TimeSpan.Zero;
            set => CacheDurationString = value.ToString();
        }
    }
}
