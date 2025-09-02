using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Add a new person into the list of persons
        /// </summary>
        /// <param name="request">Person to add</param>
        /// <returns>Returns the same person details, along with newly generated PersonID</returns>
        PersonResponse? AddPerson(PersonAddRequest? request);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons ( );

        /// <summary>
        /// Returns the person object based on the given person id
        /// </summary>
        /// <param name="personID">Person id to search</param>
        /// <returns>Returns matching person object</returns>
        PersonResponse? GetPersonByPersonID ( Guid? personID );
        
        /// <summary>
        /// Retrieves a list of persons filtered based on the specified criteria.
        /// </summary>
        /// <remarks>The filtering is case-insensitive and may vary depending on the implementation of the
        /// data source.</remarks>
        /// <param name="searchBy">The field to filter by. Common values include "Name", "Age", or "City".</param>
        /// <param name="searchString">The value to match against the specified field. Can be <see langword="null"/> or empty to retrieve all
        /// persons.</param>
        /// <returns>A list of <see cref="PersonResponse"/> objects that match the specified filter criteria.  Returns an empty
        /// list if no matches are found.</returns>
        List<PersonResponse> GetFilteredPersons ( string searchBy, string? searchString );

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents list of persons to sort</param>
        /// <param name="sortBy">Name of the property (key), based on which the persons should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns sorted persons as PersonResponse list</returns>
        List<PersonResponse> GetSortedPersons ( List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder );
    }
}