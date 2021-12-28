// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.Infrastructure
{
    /// <summary>
    /// A notification is sent to the JSON-RPC service when no response is expected or desired.
    /// </summary>
    /// <typeparam name="TParams">The type of the method parameters.</typeparam>
    public class JsonRpcNotification<TParams>
    {
        /// <summary>
        /// The name of the JSON-RPC method to call.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// A serializable type that encapsulates the parameters, if any, to the JSON-RPC service method.
        /// </summary>
        [JsonPropertyName("params")]
        public TParams Parameters { get; set; }

        /// <summary>
        /// The JSON-RPC version in use. 
        /// Default and currently only supported version is 2.0.
        /// </summary>
        [JsonPropertyName("jsonrpc")]
        public string Version => "2.0";
    }
}
