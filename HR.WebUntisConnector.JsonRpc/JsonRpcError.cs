// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.JsonRpc
{
    /// <summary>
    /// Encapsulates the details of an error that occurred on the server.
    /// </summary>
    public class JsonRpcError
    {
        /// <summary>
        /// A numeric code that identifies the type of error that occurred.
        /// <para>The pre-defined errors in the reserved -32768 to -32000 range are:
        /// -32700: Parse error,
        /// -32600: Invalid request,
        /// -32601: Method not found,
        /// -32602: Invalid params,
        /// -32603: Internal error,
        /// -3200 to -32099: Server error.
        /// </para>
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// A string that describes the error in more detail.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Contains any additional data relating to the error.
        /// The value of this property is defined by the server.
        /// </summary>
        public object Data { get; set; }
    }
}
