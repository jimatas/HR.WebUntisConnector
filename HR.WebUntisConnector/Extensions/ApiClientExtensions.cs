using HR.WebUntisConnector.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.WebUntisConnector.Extensions
{
    /// <summary>
    /// Defines some useful extension methods on the <see cref="IApiClient"/> interface.
    /// </summary>
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
        /// Retrieves all timetables for the element with the specified type and ID that fall between the specified start and end dates.
        /// This method will return all the available timetable information (bookings, substitutions, etc.) from WebUntis.
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
            var timetables = new List<Timetable>();
            var elementIds = new HashSet<int>() { elementId };

            foreach (var dateRange in (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).ToDateTimeRanges(startDate, endDate))
            {
                await RemapElementIdAsync(dateRange).ConfigureAwait(false);
                elementIds.Add(elementId);

                timetables.AddRange(await apiClient.GetTimetablesInternalAsync(elementType, elementId.ToString(), KeyTypes.Id, dateRange, cancellationToken).ConfigureAwait(false));
            }

            return (Timetables: timetables.OrderBy(table => table.Date).ThenBy(table => table.StartTime), ElementIds: elementIds);

            // Remaps the element ID of a Klasse object so that it can be found in other school years as well as in its own.
            async Task RemapElementIdAsync(DateTimeRange dateRange)
            {
                if (elementType == ElementType.Klasse)
                {
                    var (klasse, schoolYear) = await apiClient.GetKlasseByIdFromAnySchoolYearAsync(elementId, cancellationToken).ConfigureAwait(false);
                    if (klasse != null && !schoolYear.ToDateTimeRange().Includes(dateRange))
                    {
                        schoolYear = (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).Single(y => y.ToDateTimeRange().Includes(dateRange));
                        klasse = await apiClient.GetKlasseByNameFromSchoolYearAsync(klasse.Name, schoolYear, cancellationToken).ConfigureAwait(false);
                        if (klasse != null)
                        {
                            elementId = klasse.Id;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves all timetables for the element with the specified type and abbreviated name that fall between the specified start and end dates.
        /// This method will return all the available timetable information (bookings, substitutions, etc.) from WebUntis.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="elementType"></param>
        /// <param name="elementName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Timetable>> GetTimetablesAsync(this IApiClient apiClient, ElementType elementType, string elementName, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var timetables = new List<Timetable>();

            foreach (var dateRange in (await apiClient.GetSchoolYearsAsync(cancellationToken).ConfigureAwait(false)).ToDateTimeRanges(startDate, endDate))
            {
                timetables.AddRange(await apiClient.GetTimetablesInternalAsync(elementType, elementName, KeyTypes.Name, dateRange, cancellationToken).ConfigureAwait(false));
            }

            return timetables.OrderBy(table => table.Date).ThenBy(table => table.StartTime);
        }

        private static async Task<IEnumerable<Timetable>> GetTimetablesInternalAsync(this IApiClient apiClient,
            ElementType elementType,
            string elementIdOrName,
            string keyType,
            DateTimeRange dateRange,
            CancellationToken cancellationToken)
        {
            return await apiClient.GetTimetablesAsync(
                parameters: new ComprehensiveTimetableParameters()
                {
                    Options = new ComprehensiveTimetableOptions()
                    {
                        Element = new ComprehensiveTimetableElement()
                        {
                            Id = elementIdOrName,
                            KeyType = keyType,
                            Type = elementType
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

                        StartDate = int.Parse(dateRange.Start.ToString("yyyyMMdd")),
                        EndDate = int.Parse(dateRange.End.ToString("yyyyMMdd"))
                    }
                }, cancellationToken
            ).ConfigureAwait(false);
        }
    }
}
