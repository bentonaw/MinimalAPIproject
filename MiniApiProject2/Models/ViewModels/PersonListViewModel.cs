﻿namespace MiniApiProject2.Models.ViewModels
{
    public class PersonListViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PhoneNumberViewModel> PhoneNumbers { get; set; }
    }
}
