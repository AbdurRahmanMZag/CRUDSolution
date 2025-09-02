using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
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

        public List<PersonResponse> GetFilteredPersons ( string searchBy, string? searchString )
        {
            List<PersonResponse> allPersons = GetAllPersons ( );
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty ( searchBy ) || string.IsNullOrEmpty ( searchString ))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof ( Person.PersonName ):
                    
                        matchingPersons = matchingPersons
                        .Where(x=>
                            (!string.IsNullOrEmpty(x.PersonName))?
                            x.PersonName.Contains(searchString, comparisonType: StringComparison.OrdinalIgnoreCase): true).ToList();
                    break;

                case nameof ( Person.Email ):

                    matchingPersons = matchingPersons
                    .Where ( x => 
                        (!string.IsNullOrEmpty ( x.Email )) ?
                        x.Email.Contains ( searchString, comparisonType: StringComparison.OrdinalIgnoreCase ) : true ).ToList ( );
                    break;
                case nameof ( Person.DateOfBirth ):

                    matchingPersons = matchingPersons
                    .Where ( x => 
                        (x.DateOfBirth != null) ?
                        x.DateOfBirth.Value.ToString("dd-MM-yyyy").Contains ( searchString, comparisonType: StringComparison.OrdinalIgnoreCase ) : true ).ToList ( );
                    break;
                case nameof ( Person.Gender ):

                    matchingPersons = matchingPersons
                    .Where ( x => 
                        (!string.IsNullOrEmpty ( x.Gender )) ?
                        x.Gender.Contains ( searchString, comparisonType: StringComparison.OrdinalIgnoreCase ) : true ).ToList ( );
                    break;
                case nameof ( Person.CountryID ):

                    matchingPersons = matchingPersons
                    .Where ( x => 
                        (!string.IsNullOrEmpty ( x.Country )) ?
                        x.Country.Contains ( searchString, comparisonType: StringComparison.OrdinalIgnoreCase ) : true ).ToList ( );
                    break;
                case nameof ( Person.Address ):

                    matchingPersons = matchingPersons
                    .Where ( x => 
                        (!string.IsNullOrEmpty ( x.Address )) ?
                        x.Address.Contains ( searchString, comparisonType: StringComparison.OrdinalIgnoreCase ) : true ).ToList ( );
                    break;
                default: matchingPersons = allPersons;
                    break;
            }

            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons ( List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder )
        {
            if( string.IsNullOrEmpty(sortBy ) )
            {
                return allPersons;
            }

            List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof( Person.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(x => x.PersonName, StringComparer.OrdinalIgnoreCase).ToList ( ),
                (nameof( PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(x => x.PersonName, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Email ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.Email, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Email ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.Email, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Address ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.Address, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Address ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.Address, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.DateOfBirth ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.DateOfBirth ).ToList ( ),
                (nameof ( PersonResponse.DateOfBirth ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.DateOfBirth ).ToList ( ),
                (nameof ( PersonResponse.Gender ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.Gender, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Gender ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.Gender, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof( PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(x => x.ReceiveNewsLetters).ToList ( ),
                (nameof( PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(x => x.ReceiveNewsLetters).ToList ( ),
                (nameof ( PersonResponse.Country ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.Country, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Country ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.Country, StringComparer.OrdinalIgnoreCase ).ToList ( ),
                (nameof ( PersonResponse.Age ), SortOrderOptions.ASC ) => allPersons.OrderBy ( x => x.Age ).ToList ( ),
                (nameof ( PersonResponse.Age ), SortOrderOptions.DESC ) => allPersons.OrderByDescending ( x => x.Age ).ToList ( ),


                _ => allPersons
            };
            return sortedPersons;
        }
    }
}
