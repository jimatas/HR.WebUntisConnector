namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// Defines constants for the lowercased field names of an element.
    /// </summary>
    public static class ElementFields
    {
        /// <summary>
        /// The field that contains the element's primary key in the WebUntis database.
        /// </summary>
        public const string Id = "id";

        /// <summary>
        /// The field that contains the abbreviated name of the element.
        /// </summary>
        public const string Name = "name";

        /// <summary>
        /// The field that contains the full name of the element.
        /// </summary>
        public const string LongName = "longname";

        /// <summary>
        /// The field that contains the external key of the element.
        /// </summary>
        public const string ExternalKey = "externalkey";
    }
}
