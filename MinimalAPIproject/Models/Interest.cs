using System.ComponentModel.DataAnnotations;

namespace MinimalAPIproject.Models
{
    public class Interest
    {
        [Key]
        public int InternalInterestId { get; set; }
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // One-to-Many relationship: One interest can have multiple links
        public List<InterestLink> InterestLinks { get; set; }

        // Many-to-Many relation between interests and people
        public List<PersonInterest> PersonInterests { get; set; }

    }
}
