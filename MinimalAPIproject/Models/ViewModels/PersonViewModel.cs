namespace MinimalAPIproject.Models.ViewModels
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
        public List<InterestViewModel> Interests { get; set; }
    }
}
