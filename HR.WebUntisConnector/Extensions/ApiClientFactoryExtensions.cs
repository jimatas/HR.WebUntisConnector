// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector.Extensions
{
    /// <summary>
    /// Defines some useful extension methods on the <see cref="IApiClientFactory"/> interface.
    /// </summary>
    public static class ApiClientFactoryExtensions
    {
        /// <summary>
        /// Returns a new <see cref="IApiClient"/> instance that is initialized from configuration data and has been authenticated for use with the specified WebUntis school.
        /// </summary>
        /// <param name="apiClientFactory"></param>
        /// <param name="schoolOrInstituteName">The name of the WebUntis school to connect to or, alternatively, the name of a RUAS institute that maps to that WebUntis school.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that will return the authenticated <see cref="IApiClient"/> instance when completed.</returns>
        public static async Task<IApiClient> CreateApiClientAndLogInAsync(this IApiClientFactory apiClientFactory, string schoolOrInstituteName, CancellationToken cancellationToken = default)
        {
            var apiClient = apiClientFactory.CreateApiClient(schoolOrInstituteName, out var userName, out var password);
            await apiClient.LogInAsync(userName, password, cancellationToken).ConfigureAwait(false);
            return apiClient;
        }
    }
}
