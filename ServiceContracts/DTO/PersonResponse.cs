using Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the specified object is a <c>PersonResponse</c> and all properties match;
        /// otherwise, <see langword="false"/>.</returns>
        public override bool Equals ( object? obj )
        {
            if ( obj == null ) return false;
            if ( obj is not PersonResponse ) return false;
            PersonResponse person_to_compare = (PersonResponse)obj;

            return PersonID == person_to_compare.PersonID &&
                PersonName == person_to_compare.PersonName &&
                Email == person_to_compare.Email &&
                Gender == person_to_compare.Gender &&
                Address == person_to_compare.Address &&
                CountryID == person_to_compare.CountryID &&
                DateOfBirth == person_to_compare.DateOfBirth &&
                ReceiveNewsLetters == person_to_compare.ReceiveNewsLetters;

        }

        public override int GetHashCode ( )
        {
            return base.GetHashCode ( );
        }

        public override string ToString ( )
        {
            return $"Person ID: {PersonID}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DateOfBirth?.ToString ( "dd MMM yyyy" )}, Gender: {Gender}, Country ID: {CountryID}, Country: {Country}, Address: {Address}, Receive News Letters: {ReceiveNewsLetters}";
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// Converts a <see cref="Person"/> object to a <see cref="PersonResponse"/> object.
        /// </summary>
        /// <remarks>This method maps the properties of the <see cref="Person"/> object to the
        /// corresponding properties of the <see cref="PersonResponse"/> object. The <c>Age</c> property in the response
        /// is calculated based on the <c>DateOfBirth</c> property, if available.</remarks>
        /// <param name="person">The <see cref="Person"/> instance to convert. Cannot be <see langword="null"/>.</param>
        /// <returns>A <see cref="PersonResponse"/> object containing the mapped data from the <paramref name="person"/>
        /// instance.</returns>
        public static PersonResponse ToPersonResponse ( this Person person )
        {
            return new PersonResponse ( )
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                Gender = person.Gender,
                Address = person.Address,
                CountryID = person.CountryID,
                DateOfBirth = person.DateOfBirth,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = person.DateOfBirth.HasValue
                    ? Math.Round(((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25))
                    : null
            };
        }
    }
}
