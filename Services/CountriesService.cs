using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;
        public CountriesService ( )
        {
            _countries = new List<Country> ( );
        }
        public CountryResponse? AddCountry ( CountryAddRequest? countryAddRequest )
        {
            //Check if "countryAddRequest" is not null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException ( nameof ( countryAddRequest ) );
            }
            //Validate all properties of "countryAddRequest"
            if (string.IsNullOrEmpty ( countryAddRequest.CountryName ))
            {
                throw new ArgumentException ( nameof ( countryAddRequest.CountryName ) );
            }
            //Validations: Can't add duplicate CountryName
            if (_countries.Where ( c => c.CountryName == countryAddRequest.CountryName ).Any ( ))
            {
                throw new ArgumentException ( "Given country name already exists" );
            }
            //Convert "countryAddRequest" from "CountryAddRequest" type to "Country"
            Country country = countryAddRequest.ToCountry ( );
            //Generate a new CountryId
            country.CountryID = Guid.NewGuid ( );
            //Then add it into List<Country>
            _countries.Add ( country );
            //Return CountryResponse object with generated CountryID
            return country.ToCountryResponse ( );
        }

        public List<CountryResponse> GetAllCountries ( )
        {
            //Convert all countries from "Country" type to "CountryResponse" type
            //Return all CountryResponse objects

            return _countries.Select ( c => c.ToCountryResponse ( ) ).ToList ( );
        }

        public CountryResponse? GetCountryByCountryID ( Guid? countryID )
        {
            //Check if "CountryID" is not null
            //Get matching country from List<Country> based on countryID
            //Convert matching country obj from "Country" to "CountryResponse" type
            //Return CountryResponse object

            if (countryID == null)
                return null;

            Country? country = _countries.FirstOrDefault ( c => c.CountryID == countryID );
            if (country == null)
                return null;

            return (CountryResponse?)country.ToCountryResponse ( );

        }
    }
}
