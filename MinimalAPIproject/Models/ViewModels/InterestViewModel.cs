namespace MinimalAPIproject.Models.ViewModels
{
    public class InterestViewModel
    {
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<InterestLinkViewModel> Links { get; set; }
    }
}
