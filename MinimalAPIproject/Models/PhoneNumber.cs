using System.ComponentModel.DataAnnotations;

namespace MinimalAPIproject.Models
{
    public class PhoneNumber
    {
        [Key]
        public int InternalPhoneNumberId { get; set; }
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        // Foreign Key to Person
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
