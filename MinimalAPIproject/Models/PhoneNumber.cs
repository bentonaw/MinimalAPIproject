namespace MinimalAPIproject.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
