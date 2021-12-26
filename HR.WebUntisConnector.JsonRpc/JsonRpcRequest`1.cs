// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.JsonRpc
{
    /// <summary>
    /// Encapsulates the request to the JSON-RPC service.
    /// </summary>
    /// <typeparam name="TParams">The type of the method parameters.</typeparam>
    public class JsonRpcRequest<TParams> : JsonRpcNotification<TParams>
    {
        /// <summary>
        /// Arbitrary identifier to associate with the request. 
        /// If set, the corresponding property in the response will be set to the same value.
        /// </summary>
        public object Id { get; set; }
    }
}
