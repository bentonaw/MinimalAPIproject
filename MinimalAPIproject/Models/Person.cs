using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPIproject.Models
{
    public class Person
    {
        [Key]
        public int InternalPersonId { get; set; }
        // Not used in database but only for routing endpoints to keep internalpersonID private.
        [NotMapped]
        public int PersonId => InternalPersonId + 10;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // One-to-Many relationship: One person can have multiple phone numbers
        public List<PhoneNumber> PhoneNumbers { get; set; }

        // Many-to-Many relationship: One person can have multiple interests, and multiple people can share the same interest
        public List<PersonInterest> PersonInterests { get; set; }

    }
}
