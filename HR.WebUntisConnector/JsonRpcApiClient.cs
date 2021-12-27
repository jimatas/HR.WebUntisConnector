﻿// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using HR.WebUntisConnector.JsonRpc;
using HR.WebUntisConnector.JsonRpc.Extensions;
using HR.WebUntisConnector.Model;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// Default JSON-RPC based implementation of the <see cref="IApiClient"/> interface.
    /// </summary>
    public class JsonRpcApiClient : IApiClient
    {
        private readonly JsonRpcClient jsonRpcClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcApiClient"/> class.
        /// </summary>
        /// <param name="jsonRpcClient">The JSON-RPC client to use for connecting to the API.</param>
        public JsonRpcApiClient(JsonRpcClient jsonRpcClient)
            => this.jsonRpcClient = jsonRpcClient ?? throw new ArgumentNullException(nameof(jsonRpcClient));

        /// <inheritdoc/>
        public bool IsAuthenticated { get; private set; }

        /// <inheritdoc/>
        public async Task LogInAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            if (IsAuthenticated)
            {
                return;
            }

            var authenticateResult = await GetResultAsync<AuthenticateParameters, AuthenticateResult>("authenticate",
                parameters: new AuthenticateParameters()
                {
                    UserName = userName,
                    Password = password
                },
                cancellationToken
            ).WithoutCapturingContext();

            IsAuthenticated = !string.IsNullOrEmpty(authenticateResult?.SessionId);
            if (IsAuthenticated)
            {
                jsonRpcClient.SessionId = authenticateResult.SessionId;
            }
        }

        /// <inheritdoc/>
        public async Task LogOutAsync(CancellationToken cancellationToken = default)
        {
            if (!IsAuthenticated)
            {
                return;
            }

            // Logout used to be a notification, but WebUntis was returning a parse error (id=null).
            IsAuthenticated = await GetResultAsync<object>("logout", cancellationToken).WithoutCapturingContext() != null;
            if (!IsAuthenticated)
            {
                jsonRpcClient.SessionId = null;
            }
        }

        /// <inheritdoc/>
        public async Task<DateTime> GetLatestImportTimeAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return DateTimeOffset.FromUnixTimeMilliseconds(await GetResultAsync<long>("getLatestImportTime", cancellationToken).WithoutCapturingContext()).LocalDateTime;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Department>>("getDepartments", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Teacher>> GetTeachersAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Teacher>>("getTeachers", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Student>> GetStudentsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Student>>("getStudents", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Klasse>> GetKlassenAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Klasse>>("getKlassen", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Klasse>> GetKlassenAsync(KlasseParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<KlasseParameters, IEnumerable<Klasse>>("getKlassen", parameters, cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Room>>("getRooms", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Subject>> GetSubjectsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Subject>>("getSubjects", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Holiday>> GetHolidaysAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<Holiday>>("getHolidays", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<SchoolYear>>("getSchoolyears", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<SchoolYear> GetCurrentSchoolYearAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<SchoolYear>("getCurrentSchoolyear", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TimegridUnits>> GetTimegridsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<IEnumerable<TimegridUnits>>("getTimegridUnits", cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Timetable>> GetTimetablesAync(TimetableParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<TimetableParameters, IEnumerable<Timetable>>("getTimetable", parameters, cancellationToken).WithoutCapturingContext();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Timetable>> GetTimetablesAsync(ComprehensiveTimetableParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<ComprehensiveTimetableParameters, IEnumerable<Timetable>>("getTimetable", parameters, cancellationToken).WithoutCapturingContext();
        }

        /// <summary>
        /// Ensures that <see cref="LogInAsync(string, string, CancellationToken)"/> has been called by throwing an <see cref="UnauthenticatedException"/> if it has not.
        /// </summary>
        /// <returns><c>true</c> if authenticated.</returns>
        private bool EnsureAuthenticated() => IsAuthenticated ? true : throw new UnauthenticatedException("You need to log in first!");

        /// <summary>
        /// Retrieves one or more items of the specified type from WebUntis.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method">The API method to call.</param>
        /// <returns>The retrieved item(s).</returns>
        private async Task<TResult> GetResultAsync<TResult>(string method, CancellationToken cancellationToken) 
            => await GetResultAsync<object, TResult>(method, parameters: null, cancellationToken).WithoutCapturingContext();

        /// <summary>
        /// Retrieves one or more items of the specified type from WebUntis.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method">The API method to call.</param>
        /// <param name="parameters">The parameters to that method.</param>
        /// <returns>The retrieved item(s).</returns>
        private async Task<TResult> GetResultAsync<TParams, TResult>(string method, TParams parameters, CancellationToken cancellationToken)
        {
            try
            {
                return await jsonRpcClient.InvokeAsync<TParams, TResult>(method, parameters, cancellationToken).WithoutCapturingContext();
            }
            catch (JsonRpcException exception)
            {
                if (exception.ErrorCode == -8520) // Session expired.
                {
                    IsAuthenticated = false;
                }
                throw;
            }
        }
    }
}