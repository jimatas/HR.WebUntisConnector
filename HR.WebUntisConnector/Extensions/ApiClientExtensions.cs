// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using HR.WebUntisConnector.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector.Extensions
{
    public static class ApiClientExtensions
    {
        /// <summary>
        /// Retrieves the <see cref="Klasse"/> object whose ID matches the one specified, without knowing in which school year that <see cref="Klasse"/> is defined in WebUntis.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An awaitable task that, when completed, will return the Klasse and the SchoolYear it was found in.</returns>
        public static async Task<(Klasse Klasse, SchoolYear SchoolYear)> GetKlasseByIdFromAnySchoolYearAsync(this IApiClient apiClient, int id, CancellationToken cancellationToken = default)
        {
            Klasse klasse = null;
            SchoolYear schoolYear = null;

            foreach (var year in (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).OrderBy(y => y.StartDate))
            {
                klasse = (await apiClient.GetKlassenAsync(new KlasseParameters() { SchoolYearId = year.Id }, cancellationToken).ConfigureAwait(false)).SingleOrDefault(k => k.Id == id);
                if (klasse != null)
                {
                    schoolYear = year;
                    break;
                }
            }

            return (Klasse: klasse, SchoolYear: schoolYear);
        }

        /// <summary>
        /// Retrieves the <see cref="Klasse"/> object whose <see cref="Element.Name"/> matches the specified name, according to case-insensitive string comparison rules, and is defined in the specified <see cref="SchoolYear"/> in WebUntis.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="name"></param>
        /// <param name="schoolYear"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Klasse> GetKlasseByNameFromSchoolYearAsync(this IApiClient apiClient, string name, SchoolYear schoolYear, CancellationToken cancellationToken = default)
            => (await apiClient.GetKlassenAsync(new KlasseParameters() { SchoolYearId = schoolYear.Id }, cancellationToken).ConfigureAwait(false)).FirstOrDefault(k => k.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Retrieves all the timetables for the element with the specified type and ID that fall between the specified start and end dates.
        /// This method will provide all the available information in WebUntis in the returned timetables.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="elementType"></param>
        /// <param name="elementId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An awaitable task that, when completed, will return the timetables that matched the specified criteria and, if <paramref name="elementType"/> is <see cref="ElementType.Klasse"/>, a collection containing the element ID of the <see cref="Klasse"/> object for each school year that the date range falls in.</returns>
        public static async Task<(IEnumerable<Timetable> Timetables, IEnumerable<int> ElementIds)> GetTimetablesAsync(this IApiClient apiClient, ElementType elementType, int elementId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var timetables = new List<Model.Timetable>();
            var elementIds = new HashSet<int>() { elementId };

            foreach (var period in (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).ToDateTimeRanges(startDate, endDate))
            {
                await RemapElementIdForPeriodAsync(period).ConfigureAwait(false);
                ((ICollection<int>)elementIds).Add(elementId);

                timetables.AddRange(await GetTimetablesForPeriodAsync(period).ConfigureAwait(false));
            }

            return (Timetables: timetables.OrderBy(table => table.Date).ThenBy(table => table.StartTime), ElementIds: elementIds);

            // Remaps the element ID of a Klasse object so that it is valid in other school years as well.
            async Task RemapElementIdForPeriodAsync(DateTimeRange period)
            {
                if (elementType == ElementType.Klasse)
                {
                    var (klasse, schoolYear) = await apiClient.GetKlasseByIdFromAnySchoolYearAsync(elementId, cancellationToken).ConfigureAwait(false);
                    if (klasse != null && !schoolYear.ToDateTimeRange().Includes(period))
                    {
                        schoolYear = (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).Single(y => y.ToDateTimeRange().Includes(period));
                        klasse = await apiClient.GetKlasseByNameFromSchoolYearAsync(klasse.Name, schoolYear, cancellationToken).ConfigureAwait(false);
                        if (klasse != null)
                        {
                            elementId = klasse.Id;
                        }
                    }
                }
            }

            async Task<IEnumerable<Timetable>> GetTimetablesForPeriodAsync(DateTimeRange period)
            {
                return await apiClient.GetTimetablesAsync(
                    parameters: new ComprehensiveTimetableParameters()
                    {
                        Options = new ComprehensiveTimetableOptions()
                        {
                            Element = new ComprehensiveTimetableElement()
                            {
                                Id = elementId.ToString(),
                                KeyType = KeyTypes.Id,
                                Type = (int)elementType
                            },

                            ShowOnlyBaseTimetable = false,
                            ShowLessonNumber = true,
                            ShowLessonText = true,
                            ShowBooking = true,
                            ShowInformation = true,
                            ShowStudentGroup = true,
                            ShowSubstitutionText = true,

                            KlasseFields = new[] { ElementFields.Id, ElementFields.Name, ElementFields.LongName },
                            TeacherFields = new[] { ElementFields.Id, ElementFields.Name, ElementFields.LongName },
                            RoomFields = new[] { ElementFields.Id, ElementFields.Name, ElementFields.LongName },
                            SubjectFields = new[] { ElementFields.Id, ElementFields.Name, ElementFields.LongName },

                            StartDate = int.Parse(period.Start.ToString("yyyyMMdd")),
                            EndDate = int.Parse(period.End.ToString("yyyyMMdd"))
                        }
                    }, cancellationToken
                ).ConfigureAwait(false);
            }
        }
    }
}
