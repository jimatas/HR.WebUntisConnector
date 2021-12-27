// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// Thrown by the <see cref="ApiClient"/> class when an API method is invoked before a call to <see cref="IApiClient.LogInAsync(string, string, System.Threading.CancellationToken)"/>.
    /// </summary>
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException() { }
        public UnauthenticatedException(string message) : base(message) { }
        public UnauthenticatedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
