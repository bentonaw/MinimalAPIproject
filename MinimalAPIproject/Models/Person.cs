namespace MinimalAPIproject.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // one to many relation between a person and multiple phonenumbers
        public List<PhoneNumber> PhoneNumbers { get; set; }

        // many to many relation between people and interests
        public List<PersonInterest> PersonInterests { get; set; }
        //// many to many relation between people and interests (as seen in conjunction with the navigation type in the interest class
        //public List<Interest> Interests { get; set; }
        //// one to many relation between person and links of interest
        //public List<PersonInterest> InterestLinks { get; set; }
    }
}
