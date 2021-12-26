﻿// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector
{
    /// <summary>
    /// Defines the interface for a higher level abstraction for working with the WebUntis API.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Indicates whether a session was already started for the authenticated user.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Log in on the WebUntis API using the specified username and password.
        /// This is the first method that should be called, before any other API method.
        /// </summary>
        /// <param name="userName">The username to authenticate with.</param>
        /// <param name="password">The password to authenticate with.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that represents the asynchronous operation.</returns>
        Task LogInAsync(string userName, string password, CancellationToken cancellationToken = default);

        /// <summary>
        /// Log out of the WebUntis API, ending the session of the authenticated user.
        /// It is good practice to finish up your work by calling this method.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that represents the asynchronous operation.</returns>
        Task LogOutAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a date/time value, expressed in the computer's local time, denoting the last time an import from Untis took place.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return a <see cref="DateTime"/> object representing the last time schedule information was imported from Untis.</returns>
        Task<DateTime> GetLatestImportTimeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all departments from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the departments that were retrieved.</returns>
        Task<IEnumerable<Model.Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all teachers from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the teachers that were retrieved.</returns>
        Task<IEnumerable<Model.Teacher>> GetTeachersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all students from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the students that were retrieved.</returns>
        Task<IEnumerable<Model.Student>> GetStudentsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all classes from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the classes that were retrieved.</returns>
        Task<IEnumerable<Model.Klasse>> GetKlassenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all classes for a particular school year from WebUntis.
        /// </summary>
        /// <param name="parameters">Specifies the school year for which to retrieve classes.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the classes that were retrieved.</returns>
        Task<IEnumerable<Model.Klasse>> GetKlassen(Model.KlasseParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all classrooms from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the classrooms that were retrieved.</returns>
        Task<IEnumerable<Model.Room>> GetRoomsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all subjects from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the subjects that were retrieved.</returns>
        Task<IEnumerable<Model.Subject>> GetSubjectsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all holidays from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the holidays that were retrieved.</returns>
        Task<IEnumerable<Model.Holiday>> GetHolidaysAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all school years from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the school years that were retrieved.</returns>
        Task<IEnumerable<Model.SchoolYear>> GetSchoolYearsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the current school year from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return the current school year that was retrieved.</returns>
        Task<Model.SchoolYear> GetCurrentSchoolYearAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all timegrids from WebUntis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the timegrids that were retrieved.</returns>
        Task<IEnumerable<Model.TimegridUnits>> GetTimegridsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all timetables for a particular element from WebUntis.
        /// </summary>
        /// <param name="parameters">The parameters by which to determine the timetables to return.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the timetables that were retrieved.</returns>
        Task<IEnumerable<Model.Timetable>> GetTimetablesAync(Model.TimetableParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all comprehensive timetables that match the specified criteria from WebUntis.
        /// </summary>
        /// <param name="parameters">Contains the various options that specify which timetables to return and what they contain.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>An awaitable task that, when completed, will return an enumerable collection containing the timetables that were retrieved.</returns>
        Task<IEnumerable<Model.Timetable>> GetTimetablesAsync(Model.ComprehensiveTimetableParameters parameters, CancellationToken cancellationToken = default);
    }
}