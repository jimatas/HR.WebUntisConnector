// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

namespace HR.WebUntisConnector.Model
{
    /// <summary>
    /// The result returned by the <c>authenticate</c> method.
    /// </summary>
    public class AuthenticateResult
    {
        /// <summary>
        /// The session ID to supply in all the subsequent API calls.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// An integer that identifies the person type of the authenticated user. 
        /// Returns <c>0</c> if the credentials did not belong to a person.
        /// </summary>
        public int PersonType { get; set; }

        /// <summary>
        /// The person ID of the user that was authenticated.
        /// Returns <c>-1</c> if the credentials did not belong to a person.
        /// </summary>
        public int PersonId { get; set; }
    }
}
