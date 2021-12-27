// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using HR.WebUntisConnector.Configuration;
using HR.WebUntisConnector.JsonRpc;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace HR.WebUntisConnector.DependencyInjection
{
    /// <summary>
    /// Default implementation of the <see cref="IApiClientFactory"/> interface that uses the system DI container to resolve <see cref="IApiClient"/> objects.
    /// </summary>
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly WebUntisConfigurationSection configuration;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        public ApiClientFactory(IServiceProvider serviceProvider, WebUntisConfigurationSection configuration)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <inheritdoc/>
        public IApiClient CreateApiClient(string schoolOrInstituteName, out string userName, out string password)
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var serializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();

            GetConfigurationSettings(schoolOrInstituteName, out var schoolName, out userName, out password);
            var httpClient = httpClientFactory.CreateClient(schoolName);

            return new ApiClient(new JsonRpcClient(httpClient, serializerOptions));
        }

        private void GetConfigurationSettings(string schoolOrInstituteName, out string schoolName, out string userName, out string password)
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
                throw new ConfigurationErrorsException($"A <school> element with the name \"{schoolOrInstituteName}\" was not found.");
            }

            if (string.IsNullOrEmpty(userName))
            {
                ThrowMissingSettingException(nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                ThrowMissingSettingException(nameof(password));
            }

            void ThrowMissingSettingException(string settingName) => throw new ConfigurationErrorsException($"The {settingName} setting is required. "
                + "It must be specified on the <webuntis> root element or as a possible override on any of the <school> elements under it.");
        }
    }
}
