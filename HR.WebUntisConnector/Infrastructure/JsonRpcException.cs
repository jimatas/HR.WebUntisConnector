// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;

namespace HR.WebUntisConnector.Infrastructure
{
    /// <summary>
    /// Thrown to indicate that an error occurred in one of the methods of <see cref="JsonRpcClient"/> class that do not return a <see cref="JsonRpcResponse{TResult}"/>.
    /// </summary>
    public class JsonRpcException : Exception
    {
        public JsonRpcException(string message) : base(message) { }
        public JsonRpcException(int code, string message) : base(message) { ErrorCode = code; }
        public JsonRpcException(Exception innerException) : base(message: null, innerException) { }

        /// <summary>
        /// Creates a new <see cref="JsonRpcException"/> from the specified <see cref="JsonRpcError"/>.
        /// </summary>
        /// <param name="error">The JSON-RPC error that was encountered in the response.</param>
        /// <returns>A new <see cref="JsonRpcException"/> that is initialized from the specified error.</returns>
        public static JsonRpcException FromError(JsonRpcError error)
            => new JsonRpcException(error.Code, error.Message) { ErrorData = error.Data };

        /// <summary>
        /// A numeric code that identifies the type of error that occurred on the server.
        /// </summary>
        /// <seealso cref="JsonRpcError.Code"/>
        public int ErrorCode { get; }

        /// <summary>
        /// Contains any additional data relating to the error, as provided by the server.
        /// </summary>
        /// <seealso cref="JsonRpcError.Data"/>
        public object ErrorData { get; private set; }
    }
}
