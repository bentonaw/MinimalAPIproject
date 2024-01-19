
using System.ComponentModel.DataAnnotations;

namespace MiniApiProject2.Models
{
    public class InterestUrlLink
    {
        [Key]
        public int InterestUrlLinkId { get; set; }
        public string LinkToInterest { get; set; }

        // One link can only be connected to one person and interest
        public virtual Person Person { get; set; }
        public virtual Interest Interest { get; set; }

    }
}
