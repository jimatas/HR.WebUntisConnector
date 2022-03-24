using HR.WebUntisConnector.Configuration;
using HR.WebUntisConnector.Infrastructure;

using Microsoft.Extensions.Caching.Memory;

using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// Default implementation of the <see cref="IApiClientFactory"/> interface that creates JSON-RPC based <see cref="IApiClient"/> objects.
    /// </summary>
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMemoryCache memoryCache;
        private readonly WebUntisConfigurationSection configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientFactory"/> class.
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="memoryCache"></param>
        /// <param name="configuration"></param>
        public ApiClientFactory(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, WebUntisConfigurationSection configuration)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <inheritdoc/>
        public IApiClient CreateApiClient(string schoolOrInstituteName, out string userName, out string password)
        {
            GetConfiguredValues(schoolOrInstituteName, out var schoolName, out userName, out password, out var cacheDuration);
            var httpClient = httpClientFactory.CreateClient(schoolName);

            return new ApiClient(new JsonRpcClient(httpClient, CreateDefaultSerializerOptions()), memoryCache, cacheDuration);

            JsonSerializerOptions CreateDefaultSerializerOptions() => new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                AllowTrailingCommas = true
            };
        }

        /// <summary>
        /// Reads and validates the specified configuration settings, assigning their values to the appropriate out parameters.
        /// </summary>
        /// <param name="schoolOrInstituteName"></param>
        /// <param name="schoolName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="cacheDuration"></param>
        private void GetConfiguredValues(string schoolOrInstituteName, out string schoolName, out string userName, out string password, out TimeSpan cacheDuration)
        {
            schoolName = null;
            userName = configuration.UserName;
            password = configuration.Password;

            foreach (var school in configuration.Schools)
            {
                if (school.Name.Equals(schoolOrInstituteName, StringComparison.OrdinalIgnoreCase) ||
                    school.Institutes.Any(institute => institute.Name.Equals(schoolOrInstituteName, StringComparison.OrdinalIgnoreCase)))
                {
                    schoolName = school.Name;

                    if (!string.IsNullOrEmpty(school.UserName))
                    {
                        userName = school.UserName;
                    }

                    if (!string.IsNullOrEmpty(school.Password))
                    {
                        password = school.Password;
                    }

                    break;
                }
            }

            if (string.IsNullOrEmpty(schoolName))
            {
                throw new ConfigurationErrorsException($"No <school> or <institute> element with the name \"{schoolOrInstituteName}\" has been configured.");
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw MissingSettingException(nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw MissingSettingException(nameof(password));
            }

            Exception MissingSettingException(string settingName) => new ConfigurationErrorsException($"The {settingName} setting is required. "
                + "It must be specified on the <webuntis> root element or as a possible override on any of the <school> elements under it.");

            if (string.IsNullOrEmpty(configuration.CacheDurationString))
            {
                cacheDuration = TimeSpan.Zero;
            }
            else if (configuration.CacheDuration < TimeSpan.Zero)
            {
                throw new ConfigurationErrorsException("The cacheDuration setting, if specified, must be set to a positive TimeSpan value or TimeSpan.Zero.");
            }
            else
            {
                cacheDuration = configuration.CacheDuration;
            }
        }
    }
}
