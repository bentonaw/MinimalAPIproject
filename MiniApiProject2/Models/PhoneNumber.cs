
using System.ComponentModel.DataAnnotations;

namespace MiniApiProject2.Models
{
    public class PhoneNumber
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        // One phonenumber can only be connected to one person
        public virtual Person Person { get; set; }
    }
}
