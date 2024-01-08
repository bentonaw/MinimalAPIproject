namespace MinimalAPIproject.Models
{
    public class InterestLink
    {
        public int InterestLinkId { get; set; }
        public string LinkToInterest { get; set; }

        // Foreign Keys to PersonInterest
        public int PersonId { get; set; }
        public int InterestId { get; set; }
        public int PersonToInterestId {  get; set; }
        // Navigation property to specific person. 
        public Person Person { get; set; }
        public Interest Interest { get; set; }
        public PersonToInterest PersonToInterest { get; set; }

    }
}
