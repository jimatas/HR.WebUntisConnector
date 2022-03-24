using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// The parameters to the <c>authenticate</c> method.
    /// </summary>
    public class AuthenticateParameters
    {
        /// <summary>
        /// The user name to authenticate with.
        /// </summary>
        [JsonPropertyName("user")]
        public string UserName { get; set; }

        /// <summary>
        /// The password to authenticate with.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A string that identifies the client that is being connected with.
        /// Default value is <c>HR.WebUntisConnector.ApiClient</c>.
        /// </summary>
        public string Client { get; set; } = "HR.WebUntisConnector.ApiClient";
    }
}
