﻿using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
       
        //Constructor
        public CountriesServiceTest ( )
        {
            _countriesService = new CountriesService ( );
        }

        #region AddCountry

        //When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry ( )
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException> ( ( ) =>
            {
                //Act
                _countriesService.AddCountry ( request );
            } );
        }

        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull ( )
        {
            //Arrange
            CountryAddRequest? request = new ( ) { CountryName = null };

            //Assert
            Assert.Throws<ArgumentException> ( ( ) =>
            {
                //Act
                _countriesService.AddCountry ( request );
            } );
        }


        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName ( )
        {
            //Arrange
            CountryAddRequest? request1 = new ( ) { CountryName = "Egypt" };
            CountryAddRequest? request2 = new ( ) { CountryName = "Egypt" };

            //Assert
            Assert.Throws<ArgumentException> ( ( ) =>
            {
                //Act
                _countriesService.AddCountry ( request1 );
                _countriesService.AddCountry ( request2 );
            } );
        }


        //When you supply proper country name, it should insert (add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails ( )
        {
            //Arrange
            CountryAddRequest? request = new ( ) { CountryName = "Syria" };

            //Act
            CountryResponse response = _countriesService.AddCountry ( request );
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries ( );

            //Assert
            Assert.True ( response.CountryID != Guid.Empty );
            Assert.Contains(response, countries_from_GetAllCountries);
            
        }

        #endregion



        #region GetAllCountries
        [Fact]
        public void GetAllCountries_EmptyList ( )
        {
            //Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries ( );

            //Assert
            Assert.Empty ( actual_country_response_list );
        }

        [Fact]
        public void GetAllCountries_AddFewCountries ( )
        {

            List<CountryAddRequest> country_request_list = new List<CountryAddRequest> ( ) {
                    new CountryAddRequest() { CountryName = "Egypt"},
                    new CountryAddRequest() { CountryName = "Syria"}
                };

            //Act
            List<CountryResponse> country_list_from_add_country = new List<CountryResponse> ( );
            foreach (CountryAddRequest countryRequest in country_request_list)
            {
                country_list_from_add_country.Add( _countriesService.AddCountry( countryRequest ));
            }

            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries ( );

            //read each element from countries_list_from_add_country
            foreach(CountryResponse expected_country in country_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList );
            }
        }
        #endregion


        #region GetCountryByCountryID

        [Fact]
        //If we supply null as CountryID, it should returns null as CountryResponse
        public void GetCountryByCountryID_NullCountryID( )
        {
            //Arrange
            Guid? countryID = null;

            //Act
            CountryResponse? country_response_from_GetCountryByCountryID = _countriesService.GetCountryByCountryID ( countryID );

            //Assert
            Assert.Null(country_response_from_GetCountryByCountryID);

        }

        [Fact]
        public void GetCountryByCountryID_ValidCountryID( )
        {
            //Arrange
            CountryAddRequest country_add_request = new CountryAddRequest ( )
            {
                CountryName = "Egypt"
            };

            CountryResponse country_response_from_add_country = _countriesService.AddCountry ( country_add_request );

            //Act 
            CountryResponse? actualCountryResponse = _countriesService.GetCountryByCountryID ( country_response_from_add_country.CountryID );

            //Assert
            Assert.Equal(country_response_from_add_country, actualCountryResponse );
        }
        #endregion
    }
}
