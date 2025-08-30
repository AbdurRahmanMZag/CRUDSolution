using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the country object after adding it ( including the newly generated country id )</returns>
        CountryResponse? AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Retrieves a list of all available countries.
        /// </summary>
        /// <returns>A list of <see cref="CountryResponse"/> objects representing all countries.  The list will be empty if no
        /// countries are available.</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Returns a country obj based on the given country id
        /// </summary>
        /// <param name="countryID">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}
