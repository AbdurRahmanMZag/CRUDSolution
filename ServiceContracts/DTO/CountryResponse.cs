
using Entities;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is use as return type for almost all of CountriesService Class methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        //It compares the current obj to another obj of CountryResponse type and returns true, if both values are same; otherwise returns false
        public override bool Equals ( object? obj )
        {
            if ( obj == null)
            {
                return false;
            }
            if(obj.GetType() != typeof( CountryResponse ) )
            {
                return false; 
            }

            CountryResponse country_to_compare = (CountryResponse)obj;
            return CountryID == country_to_compare.CountryID &&
                CountryName == country_to_compare.CountryName;
        }

        public override int GetHashCode ( )
        {
            return base.GetHashCode ( );
        }
    }

    public static class CountryExtensions
    {
        //Converts from Country obj to CountryResponse obj
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }

}
