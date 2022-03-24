using HR.WebUntisConnector.Infrastructure;
using HR.WebUntisConnector.Model;

using Microsoft.Extensions.Caching.Memory;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// Default implementation of the <see cref="IApiClient"/> interface that communicates directly with the WebUntis JSON-RPC service.
    /// </summary>
    public class ApiClient : IApiClient
    {
        private readonly JsonRpcClient jsonRpcClient;
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan cacheDuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="jsonRpcClient">The JSON-RPC client to connect to the WebUntis API with.</param>
        /// <param name="memoryCache">The memory cache to cache the results of parameterless API methods in.</param>
        /// <param name="cacheDuration">The duration for which to cache the results. A value of <see cref="TimeSpan.Zero"/>, or less, disables caching.</param>
        public ApiClient(JsonRpcClient jsonRpcClient, IMemoryCache memoryCache, TimeSpan cacheDuration)
        {
            this.jsonRpcClient = jsonRpcClient ?? throw new ArgumentNullException(nameof(jsonRpcClient));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.cacheDuration = cacheDuration;
        }

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
            ).ConfigureAwait(false);

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
            IsAuthenticated = await GetResultAsync<object>("logout", cancellationToken).ConfigureAwait(false) != null;
            if (!IsAuthenticated)
            {
                jsonRpcClient.SessionId = null;
            }
        }

        /// <inheritdoc/>
        public async Task<DateTime> GetLatestImportTimeAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return DateTimeOffset.FromUnixTimeMilliseconds(await GetOrCreateCachedResultAsync<long>("getLatestImportTime", cancellationToken).ConfigureAwait(false)).LocalDateTime;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Department>>("getDepartments", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Teacher>> GetTeachersAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Teacher>>("getTeachers", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Student>> GetStudentsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Student>>("getStudents", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Klasse>> GetKlassenAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Klasse>>("getKlassen", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Klasse>> GetKlassenAsync(KlasseParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<KlasseParameters, IEnumerable<Klasse>>("getKlassen", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Room>>("getRooms", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Subject>> GetSubjectsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Subject>>("getSubjects", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Holiday>> GetHolidaysAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<Holiday>>("getHolidays", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<SchoolYear>>("getSchoolyears", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<SchoolYear> GetCurrentSchoolYearAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<SchoolYear>("getCurrentSchoolyear", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TimegridUnits>> GetTimegridsAsync(CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetOrCreateCachedResultAsync<IEnumerable<TimegridUnits>>("getTimegridUnits", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Timetable>> GetTimetablesAsync(TimetableParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<TimetableParameters, IEnumerable<Timetable>>("getTimetable", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Timetable>> GetTimetablesAsync(ComprehensiveTimetableParameters parameters, CancellationToken cancellationToken = default)
        {
            EnsureAuthenticated();

            return await GetResultAsync<ComprehensiveTimetableParameters, IEnumerable<Timetable>>("getTimetable", parameters, cancellationToken).ConfigureAwait(false);
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
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that will return the retrieved item(s) when it completes.</returns>
        private async Task<TResult> GetResultAsync<TResult>(string method, CancellationToken cancellationToken) 
            => await GetResultAsync<object, TResult>(method, parameters: null, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Retrieves one or more items of the specified type from WebUntis.
        /// </summary>
        /// <typeparam name="TParams"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method">The API method to call.</param>
        /// <param name="parameters">The parameters to that method.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that will return the retrieved item(s) when it completes.</returns>
        private async Task<TResult> GetResultAsync<TParams, TResult>(string method, TParams parameters, CancellationToken cancellationToken)
        {
            try
            {
                return await jsonRpcClient.InvokeAsync<TParams, TResult>(method, parameters, cancellationToken).ConfigureAwait(false);
            }
            catch (JsonRpcException exception)
            {
                if (exception.ErrorCode == -8520) // Session expired.
                {
                    IsAuthenticated = false;
                    throw new UnauthenticatedException("Session expired. You need to log in again!");
                }
                throw;
            }
        }

        /// <summary>
        /// Retrieves one or more items of the specified type from WebUntis and stores them in cache, if caching is enabled. 
        /// If the items to retrieve were already present in the cache, returns those.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<TResult> GetOrCreateCachedResultAsync<TResult>(string method, CancellationToken cancellationToken)
        {
            var cacheKey = $"webuntis[{jsonRpcClient.Url}].{method}";

            if (cacheDuration <= TimeSpan.Zero || !memoryCache.TryGetValue(cacheKey, out TResult result))
            {
                result = await GetResultAsync<TResult>(method, cancellationToken).ConfigureAwait(false);
            }

            if (cacheDuration > TimeSpan.Zero)
            {
                memoryCache.Set(cacheKey, result, cacheDuration);
            }

            return result;
        }
    }
}
