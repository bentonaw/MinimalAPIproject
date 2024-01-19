
using System.ComponentModel.DataAnnotations;

namespace MiniApiProject2.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // One person can have many phonenumbers
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        // One person can have many interests
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
