using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPIproject.Models
{
    public class PhoneNumber
    {
        //[Key]
        //public int InternalPhoneNumberId { get; set; }
        //// Not used in database but only for routing endpoints to keep internalpersonID private.
        //[NotMapped]
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        // Foreign Key to Person
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
