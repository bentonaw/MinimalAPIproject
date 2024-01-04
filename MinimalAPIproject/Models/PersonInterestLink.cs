namespace MinimalAPIproject.Models
{
    public class PersonInterestLink
    {
        public int PersonInterestLinkId { get; set; }
        public int PersonId { get; set; }
        public int InterestId { get; set; }
        // multiple links for each interest of a specific person
        public List<InterestLink> Links { get; set; }
        // each link is connected to specific person and interest
        public Person Person { get; set; }
        public Interest Interest { get; set; }
    }
}
