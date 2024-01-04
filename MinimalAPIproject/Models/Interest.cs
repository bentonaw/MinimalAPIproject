namespace MinimalAPIproject.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // many to many relation between interests and people (as seen in conjunction with the navigation type in the person class)
        public List<Person> Persons { get; set; }
        // one to many relation between interest and links
        public List<PersonInterestLink> InterestLinks { get; set; }
    }
}
