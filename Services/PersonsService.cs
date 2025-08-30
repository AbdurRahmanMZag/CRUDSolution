using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private fields
        private readonly List<Person> _persons;
        private readonly CountriesService _countriesService;

        //constructor
        public PersonsService ( )
        {
            _persons = new List<Person> ();
            _countriesService = new CountriesService ();
        }

        public PersonResponse? AddPerson ( PersonAddRequest? personAddRequest )
        {
            //Check if "personAddRequest" is not null
            //Validate all properties of "personAddRequest"
            //Convert "personAddRequest" from "PersonAddRequest" type to "Person"
            //Generate a new PersonID
            //Then add it into List<Person>
            //Return PersonResponse object with generated PersonID

            if (personAddRequest == null)
            {
                throw new ArgumentNullException ( nameof ( personAddRequest ) );
            }

            //Model validation
            ValidationHelper.ModelValidation ( personAddRequest );

            Person person = personAddRequest.ToPerson ( );
            person.PersonID = Guid.NewGuid ( );

            _persons.Add ( person );
            PersonResponse personResponse = ConvertPersonToPersonResponse ( person );

            return personResponse;

        }

        private PersonResponse ConvertPersonToPersonResponse ( Person person )
        {
            PersonResponse personResponse = person.ToPersonResponse ( );
            personResponse.Country = _countriesService.GetCountryByCountryID ( personResponse.CountryID )?.CountryName;
            return personResponse;
        }

        public List<PersonResponse> GetAllPersons ( )
        {
            //Convert all persons from "Person" type to "PersonResponse" type
            //Return all PersonResponse objects
            List<Person> people = [.. _persons];
            List<PersonResponse> personResponses = [];
            foreach ( Person person in people )
            {
                personResponses.Add ( person.ToPersonResponse ( ) );
            }

            return personResponses;
        }

        public PersonResponse? GetPersonByPersonID ( Guid? personID )
        {
            //Check if "personID" is not null.
            //Get matching person from List<Person> based on personID
            //Convert matching person object from "Person" to "PersonResponse" type
            //Return PersonResponse object

            if (personID == null)
            {
                return null;
            }
            Person? person = _persons.SingleOrDefault(x=> x.PersonID == personID);
            if (person == null) return null;

            return person.ToPersonResponse( );
        }
    }
}
