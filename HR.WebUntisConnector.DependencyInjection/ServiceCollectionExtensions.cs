// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using HR.WebUntisConnector.Configuration;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiClientFactory(this IServiceCollection services, WebUntisConfigurationSection configuration)
        {
            foreach (var school in configuration.Schools)
            {
                var serviceUrl = string.IsNullOrEmpty(school.ServiceUrl) ? configuration.ServiceUrl : school.ServiceUrl;
                if (string.IsNullOrEmpty(serviceUrl))
                {
                    throw new ConfigurationErrorsException("The serviceUrl setting is required. "
                        + "It must be specified on the <webuntis> root element or as a possible override on any of the <school> elements under it.");
                }
                serviceUrl = string.Format(serviceUrl, school.Name);

                services.AddHttpClient(school.Name, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(serviceUrl);
                }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false });
            }

            services.AddScoped<IApiClientFactory, ApiClientFactory>(provider => new ApiClientFactory(provider.GetRequiredService<IHttpClientFactory>(), CreateDefaultSerializerOptions(), configuration));

            return services;

            JsonSerializerOptions CreateDefaultSerializerOptions() => new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                AllowTrailingCommas = true
            };
        }
    }
}
