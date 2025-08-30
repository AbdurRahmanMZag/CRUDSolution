using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        //Private fields
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        //Constructor
        public PersonsServiceTest ( )
        {
            _personsService = new PersonsService ( );
            _countriesService = new CountriesService ( );
        }

        #region AddPerson

        //When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson( )
        {
            //Arrange
            PersonAddRequest? request = null;

            Assert.Throws<ArgumentNullException> ( ( ) =>
            {
                //Acts
                _personsService.AddPerson ( request );

            } );
        }


        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull ( )
        {
            //Arrange
            PersonAddRequest? request = new PersonAddRequest ( )
            {
                PersonName = null
            };

            Assert.Throws<ArgumentException> ( ( ) =>
            {
                //Acts
                _personsService.AddPerson ( request );

            } );
        }


        //When we supply proper person details, it should isert the person into the persons list; and it should return an obj of PersonResponse, which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails ( )
        {
            //Arrange
            PersonAddRequest? request = new PersonAddRequest ( )
            {
                PersonName = "person name ..",
                Email="person@example.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true

            };

            //Acts
            PersonResponse? person_response_from_add = _personsService.AddPerson ( request );

            List<PersonResponse> persons_list = _personsService.GetAllPersons ( );

            //Assert
            Assert.True(person_response_from_add!.PersonID != Guid.Empty );

            Assert.Contains ( person_response_from_add, persons_list );
            

        }

        #endregion


        #region GetPersonByPersonID

        //If we supply null as personID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID( )
        {
            //Arrange
            Guid? personID = null;
            //Act
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID ( personID );
            //Assert
            Assert.Null(person_response_from_get );

        }

        //If we supply valid personID, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID( )
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest ( )
            {
                CountryName = "Makka"
            };

            CountryResponse? country_response_from_add = _countriesService.AddCountry ( countryAddRequest );

            PersonAddRequest personAddRequest = new PersonAddRequest ( )
            {
                PersonName = "person name ...",
                Email = "email@sample.com",
                DateOfBirth = DateTime.Parse ( "2000-01-01" ),
                Address = "address",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                CountryID = country_response_from_add?.CountryID,
                ReceiveNewsLetters = true
            };
            PersonResponse? person_response_from_add = _personsService.AddPerson ( personAddRequest );

            //Act
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID ( person_response_from_add?.PersonID );

            //Assert
            Assert.Equal ( person_response_from_add, person_response_from_get );
        }

        #endregion


        #region GetAllPersons

        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList ( )
        {
            //Act
            List<PersonResponse> persons_from_get = _personsService.GetAllPersons ( );

            //Assert
            Assert.Empty ( persons_from_get );
        }


        //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
        [Fact]
        public void GetAllPersons_AddFewPersons ( )
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest ( ) { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest ( ) { CountryName = "India" };

            CountryResponse? country_response_1 = _countriesService.AddCountry ( country_request_1 );
            CountryResponse? country_response_2 = _countriesService.AddCountry ( country_request_2 );

            PersonAddRequest person_request_1 = new PersonAddRequest ( ) { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1?.CountryID, DateOfBirth = DateTime.Parse ( "2002-05-06" ), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest ( ) { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2?.CountryID, DateOfBirth = DateTime.Parse ( "2000-02-02" ), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest ( ) { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2?.CountryID, DateOfBirth = DateTime.Parse ( "1999-03-03" ), ReceiveNewsLetters = true };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest> ( ) { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse> ( );

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse? person_response = _personsService.AddPerson ( person_request );
                person_response_list_from_add.Add ( person_response! );
            }

            //Act
            List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons ( );

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains ( person_response_from_add, persons_list_from_get );
            }
        }
        #endregion
    }
}
