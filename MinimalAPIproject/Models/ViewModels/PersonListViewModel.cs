namespace MinimalAPIproject.Models.ViewModels
{
    public class PersonListViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
    }
}
