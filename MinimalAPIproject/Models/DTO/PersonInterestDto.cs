namespace MinimalAPIproject.Models.DTO
{
    public class PersonInterestDto
    {
        public int PersonId { get; set; }
        public string InterestTitle { get; set; }
        public List<string> Links { get; set; }
    }
}
