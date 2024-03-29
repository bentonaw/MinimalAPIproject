﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPIproject.Models
{
    public class Person
    {
        public int PersonId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // One-to-Many relationship: One person can have multiple phone numbers
        public List<PhoneNumber> PhoneNumbers { get; set; }

        // Many-to-Many relationship: One person can have multiple interests, and multiple people can share the same interest
        public List<PersonInterest> PersonInterests { get; set; }

    }
}
