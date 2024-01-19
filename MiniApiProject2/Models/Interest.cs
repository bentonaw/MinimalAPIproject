
using System.ComponentModel.DataAnnotations;

namespace MiniApiProject2.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // One interest can have many links
        public virtual ICollection<InterestUrlLink> InterestUrlLinks { get; set; }
        // One interest can have many persons
        public virtual ICollection<Person> Persons { get; set; }

    }
}
