namespace MinimalAPIproject.Models
{
    public class PersonInterestLink
    {
        public int PersonInterestLinkId { get; set; }
        public string LinkToInterest { get; set; }

        // Foreign Keys to PersonInterest
        public int PersonId { get; set; }
        public int InterestId { get; set; }
        // Navigation property to specific person. 
        public Person Person { get; set; }
        public Interest Interest { get; set; }

    }
}
