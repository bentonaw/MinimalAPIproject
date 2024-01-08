using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPIproject.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // One-to-Many relationship: One Person-Interest can have multiple links
        public List<PersonInterestLink> PersonInterestLinks { get; set; }

        // Many-to-Many relation between interests and people
        public List<PersonInterest> PersonInterests { get; set; }

    }
}
