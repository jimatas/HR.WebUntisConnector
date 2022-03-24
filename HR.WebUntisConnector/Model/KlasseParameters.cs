using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// The parameters to the overloaded <c>getKlassen</c> method.
    /// </summary>
    public class KlasseParameters
    {
        /// <summary>
        /// The ID of the school year for which the klassen are to be retrieved.
        /// </summary>
        [JsonPropertyName("schoolyearId")]
        public int SchoolYearId { get; set; }
    }
}
