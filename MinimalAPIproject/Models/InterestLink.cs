using System.ComponentModel.DataAnnotations;

namespace MinimalAPIproject.Models
{
    public class InterestLink
    {
        [Key]
        public int InternalInterestLinkId { get; set; }
        public int InterestLinkId { get; set; }
        public string UrlLink { get; set; }
        
        // Foreign Key to Interest
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
