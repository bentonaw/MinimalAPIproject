namespace MinimalAPIproject.Models
{
    public class PersonInterestLink
    {
        public int PersonInterestLinkId { get; set; }
        public int PersonInterestId { get; set; }
        public string Link { get; set; }

        //// one to one relation to personinterest
        //public PersonInterest PersonInterest { get; set; }

    }
}
