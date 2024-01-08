using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPIproject.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        // Foreign Key to Person
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
