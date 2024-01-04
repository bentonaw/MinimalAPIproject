namespace MinimalAPIproject.Models
{
    public class InterestLink
    {
        public int InterestLinkId { get; set; }
        public string Link { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
