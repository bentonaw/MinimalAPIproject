﻿namespace MinimalAPIproject.Models
{
    public class PersonToInterest
    {
        public int PersonToInterestId { get; set; }

        // Foreign Keys for the Many-to-Many relationship table
        public int PersonId { get; set; }
        public int InterestId { get; set; }
        
        // Navigation properties
        public Person Person { get; set; }
        public Interest Interest { get; set; }

        public InterestLink PersonInterestLink { get; set; }

        //// One-to-Many relationship: One person-interest combination can have multiple links
        //public List<PersonInterestLink> LinksToInterest { get; set; }
    }
}
