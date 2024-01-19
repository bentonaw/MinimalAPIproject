
namespace MiniApiProject2.Models.ViewModels
{
    public class InterestViewModel
    {
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InterestUrlLinkViewModel> InterestUrlLinks { get; set; }
    }
}
