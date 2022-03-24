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
