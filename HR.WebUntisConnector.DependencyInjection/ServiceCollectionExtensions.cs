using HR.WebUntisConnector.Configuration;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Configuration;
using System.Net.Http;

namespace HR.WebUntisConnector.DependencyInjection
{
    /// <summary>
    /// Extension methods on <see cref="IServiceCollection"/> to register <see cref="IApiClientFactory"/> as a service.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an <see cref="IApiClientFactory"/> to the service collection using the specified configuration and service lifetime.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Configuration data for the <see cref="IApiClient"/> objects that will be created.</param>
        /// <param name="serviceLifetime">The service lifetime to register the <see cref="IApiClientFactory"/> with.</param>
        /// <returns></returns>
        public static IServiceCollection AddApiClientFactory(this IServiceCollection services, WebUntisConfigurationSection configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
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
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    UseCookies = false
                });
            }

            services.AddMemoryCache();

            services.Add(new ServiceDescriptor(
                serviceType: typeof(IApiClientFactory),
                provider => new ApiClientFactory(provider.GetRequiredService<IHttpClientFactory>(), provider.GetRequiredService<IMemoryCache>(), configuration),
                serviceLifetime));

            return services;
        }
    }
}
