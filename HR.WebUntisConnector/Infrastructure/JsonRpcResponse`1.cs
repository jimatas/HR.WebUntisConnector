// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Infrastructure
{
    /// <summary>
    /// Encapsulates the response returned by the JSON-RPC service.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class JsonRpcResponse<TResult>
    {
        /// <summary>
        /// If present matches the identifier that was specified in the request.
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// A serializable type that represents the result returned by the JSON-RPC service method.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Contains the error details if an error occurs.
        /// </summary>
        public JsonRpcError Error { get; set; }

        /// <summary>
        /// The JSON-RPC version in use. 
        /// Default and currently only supported version is 2.0.
        /// </summary>
        [JsonPropertyName("jsonrpc")]
        public string Version => "2.0";
    }
}
