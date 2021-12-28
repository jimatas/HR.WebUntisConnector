// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// This exception is thrown by the methods of the <see cref="ApiClient"/> class when they are invoked before <see cref="ApiClient.LogInAsync(string, string, System.Threading.CancellationToken)"/> has been called first.
    /// </summary>
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException() { }
        public UnauthenticatedException(string message) : base(message) { }
        public UnauthenticatedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
