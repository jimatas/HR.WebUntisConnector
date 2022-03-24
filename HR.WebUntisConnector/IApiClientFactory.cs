namespace HR.WebUntisConnector
{
    /// <summary>
    /// Defines the interface for a factory of <see cref="IApiClient"/> objects.
    /// </summary>
    public interface IApiClientFactory
    {
        /// <summary>
        /// Returns a new instance of the <see cref="IApiClient"/> class that is initialized from configuration data.
        /// </summary>
        /// <param name="schoolOrInstituteName">The name of the WebUntis school to connect to or, alternatively, the name of a RUAS institute that maps to that WebUntis school.</param>
        /// <param name="userName">An out parameter to which the username that has been configured for the specified WebUntis school will be assigned.</param>
        /// <param name="password">An out parameter to which the password that has been configured for the specified WebUntis school will be assigned.</param>
        /// <returns>An unauthenticated <see cref="ApiClient"/> object. Use the values assigned to the <paramref name="userName"/> and <paramref name="password"/> out parameters to authenticate the instance by calling the <see cref="ApiClient.LogInAsync(string, string, System.Threading.CancellationToken)"/> method.</returns>
        IApiClient CreateApiClient(string schoolOrInstituteName, out string userName, out string password);
    }
}
