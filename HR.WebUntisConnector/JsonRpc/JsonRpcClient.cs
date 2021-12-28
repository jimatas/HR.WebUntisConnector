// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using HR.WebUntisConnector.JsonRpc.Infrastructure;

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector.JsonRpc
{
    /// <summary>
    /// A client with which to call remote methods on a JSON-RPC service.
    /// </summary>
    public class JsonRpcClient
    {
        private static readonly MediaTypeWithQualityHeaderValue acceptHeader;
        private static readonly ProductInfoHeaderValue userAgentHeader;

        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions serializerOptions;

        /// <summary>
        /// Static initializer.
        /// </summary>
        static JsonRpcClient()
        {
            acceptHeader = new MediaTypeWithQualityHeaderValue("application/json-rpc");
            userAgentHeader = new ProductInfoHeaderValue("HR.WebUntisConnector.JsonRpc.JsonRpcClient", "2.0");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to connect to the JSON-RPC service with.</param>
        /// <param name="serializerOptions">Options to the JSON serializer.</param>
        public JsonRpcClient(HttpClient httpClient, JsonSerializerOptions serializerOptions)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            SetDefaultHeaders(this.httpClient);

            this.serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
            AddCustomConverter(this.serializerOptions);
        }

        /// <summary>
        /// The URL of the JSON-RPC service.
        /// </summary>
        public Uri Url => httpClient.BaseAddress;

        /// <summary>
        /// If set, the session ID to supply when making API calls that require authentication.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Invokes a JSON-RPC method using the specified request and returns the received response.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="jsonRpcRequest">The JSON-RPC request to send.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return the received JSON-RPC response. 
        /// If an error occurs on the server, the <see cref="JsonRpcResponse{TResult}.Error"/> property will contain information about the error.</returns>
        public async Task<JsonRpcResponse<TResult>> InvokeAsync<TParams, TResult>(JsonRpcRequest<TParams> jsonRpcRequest, CancellationToken cancellationToken = default)
        {
            var httpRequest = CreateRequestMessage(jsonRpcRequest);

            var httpResponse = await httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

            var jsonRpcResponse = await httpResponse.Content.ReadFromJsonAsync<JsonRpcResponse<TResult>>(serializerOptions, cancellationToken).ConfigureAwait(false);
            return jsonRpcResponse;
        }

        /// <summary>
        /// Invokes a JSON-RPC method that accepts parameters and returns a result.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method">The name of the method to invoke.</param>
        /// <param name="parameters">The parameters to the method.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return the result returned by the server.</returns>
        public async Task<TResult> InvokeAsync<TParams, TResult>(string method, TParams parameters, CancellationToken cancellationToken = default)
        {
            var jsonRpcRequest = new JsonRpcRequest<TParams>()
            {
                Id = GenerateRandomNumber(),
                Method = method,
                Parameters = parameters
            };

            var jsonRpcResponse = await InvokeAsync<TParams, TResult>(jsonRpcRequest, cancellationToken).ConfigureAwait(false);
            if (jsonRpcResponse.Error is null)
            {
                if (jsonRpcResponse.Id.Equals(jsonRpcRequest.Id))
                {
                    return jsonRpcResponse.Result;
                }

                throw new InvalidOperationException("The server did not return the expected response. The identifier in the response did not match the one supplied in the request.");
            }

            throw JsonRpcException.FromError(jsonRpcResponse.Error);
        }

        /// <summary>
        /// Invokes a JSON-RPC method that does not accept parameters and returns a result.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method">The name of the method to invoke.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return the result returned by the server.</returns>
        public async Task<TResult> InvokeAsync<TResult>(string method, CancellationToken cancellationToken = default)
            => await InvokeAsync<object, TResult>(method, parameters: null, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Invokes a JSON-RPC method that does not return a result, or the result can be discarded.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <param name="jsonRpcNotification">The notification to send to the JSON-RPC service.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that represents the asynchronous operation.</returns>
        public async Task NotifyAsync<TParams>(JsonRpcNotification<TParams> jsonRpcNotification, CancellationToken cancellationToken = default)
            => (await httpClient.PostAsJsonAsync((Uri)null, jsonRpcNotification, cancellationToken).ConfigureAwait(false)).EnsureSuccessStatusCode();

        /// <summary>
        /// Invokes a JSON-RPC method that does not return a result, or the result can be discarded.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <param name="method">The name of the method to invoke.</param>
        /// <param name="parameters">The parameters to the method.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that represents the asynchronous operation.</returns>
        public async Task NotifyAsync<TParams>(string method, TParams parameters, CancellationToken cancellationToken = default)
        {
            var jsonRpcNotification = new JsonRpcNotification<TParams>()
            {
                Method = method,
                Parameters = parameters
            };

            await NotifyAsync(jsonRpcNotification, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes a JSON-RPC method that does not accept parameters and does not return a result, or the result can be discarded.
        /// </summary>
        /// <param name="method">The name of the method to invoke.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that represents the asynchronous operation.</returns>
        public async Task NotifyAsync(string method, CancellationToken cancellationToken = default)
            => await NotifyAsync(method, parameters: (object)null, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Creates a new HttpRequestMessage object that has its Content property set to the JSON serialized representation of the specified JsonRpcRequest object 
        /// and, if applicable, a request header added to its Headers collection with the value of the SessionId property.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <param name="jsonRpcRequest"></param>
        /// <returns></returns>
        private HttpRequestMessage CreateRequestMessage<TParams>(JsonRpcRequest<TParams> jsonRpcRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, (Uri)null);
            AddSessionCookie(httpRequest);
            httpRequest.Content = JsonContent.Create(jsonRpcRequest);

            return httpRequest;
        }

        /// <summary>
        /// Adds a cookie with the name "JSESSIONID" and the value returned by the SessionId property, if any, to the request headers of the specified HttpRequestMessage object.
        /// </summary>
        /// <param name="httpRequest"></param>
        private void AddSessionCookie(HttpRequestMessage httpRequest)
        {
            if (!string.IsNullOrEmpty(SessionId) && !(httpRequest.Headers.TryGetValues("Cookie", out var cookies) && cookies.Any(cookie => cookie.StartsWith("JSESSIONID"))))
            {
                httpRequest.Headers.TryAddWithoutValidation("Cookie", $"JSESSIONID={SessionId}");
            }
        }

        /// <summary>
        /// Generates a random 32-bit integer that is mostly within the range of the unsigned byte type.
        /// </summary>
        /// <returns>A 32-bit integer between 1 and <see cref="byte.MaxValue"/> + 1.</returns>
        private static object GenerateRandomNumber() => new Random().Next(1, byte.MaxValue + 1).ToString();

        /// <summary>
        /// Sets some default request headers on the specified HttpClient instance.
        /// </summary>
        /// <param name="httpClient"></param>
        private static void SetDefaultHeaders(HttpClient httpClient)
        {
            if (!httpClient.DefaultRequestHeaders.Accept.Contains(acceptHeader))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            }

            if (!httpClient.DefaultRequestHeaders.UserAgent.Contains(userAgentHeader))
            {
                httpClient.DefaultRequestHeaders.UserAgent.Add(userAgentHeader);
            }
        }

        /// <summary>
        /// Adds a custom ObjectJsonConverter instance, if it was not yet added, to the Converters collection of the specified JsonSerializerOptions instance.
        /// </summary>
        /// <param name="options"></param>
        private static void AddCustomConverter(JsonSerializerOptions options)
        {
            foreach (var converter in options.Converters.Where(converter => converter.CanConvert(typeof(object))))
            {
                if (converter.GetType() == typeof(ObjectJsonConverter))
                {
                    return;
                }
            }

            options.Converters.Add(new ObjectJsonConverter());
        }
    }
}
