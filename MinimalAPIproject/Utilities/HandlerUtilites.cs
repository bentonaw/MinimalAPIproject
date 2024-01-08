using MinimalAPIproject.Data;
using MinimalAPIproject.Models.ViewModels;
using MinimalAPIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPIproject.Utilities
{
    public class HandlerUtilites
    {
        // Method for finding person
        public static Person? PersonFinder(ApplicationContext context, int personId)
        {
            return context.Persons
                .Where(p => p.PersonId == personId)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonInterests)
                    .ThenInclude(pi => pi.Interest)
                        .ThenInclude(i => i.PersonInterestLinks)
                .SingleOrDefault();
        }
        // Method for mapping out interests of specific person with links attached.
        public static List<InterestViewModel> MapPersonInterests(IEnumerable<PersonInterest> personInterests, int personId)
        {
             return personInterests
                .Where(pi => pi.PersonId == personId)
                .Select(pi => new InterestViewModel
                {
                InterestId = pi.Interest.InterestId,
                Title = pi.Interest.Title,
                Description = pi.Interest.Description,
                Links = pi.Interest.PersonInterestLinks
                    .Where(l => l.InterestId == pi.Interest.InterestId)
                    .Select(l => new PersonInterestLinkViewModel
                    {
                        LinkToInterest = l.LinkToInterest
                    })
                    .ToList()
            })
            .ToList();
        }
    }
}
