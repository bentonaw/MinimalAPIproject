using MinimalAPIproject.Data;
using MinimalAPIproject.Models.ViewModels;
using MinimalAPIproject.Models;
using MinimalAPIproject.Utilities;
using MinimalAPIproject.Models.DTO;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPIproject.Handlers
{
    public class PersonInterestLinkHandler
    {
        // Returns list of links connected to specific person
        public static IResult ListLinkToInterestsOfPerson(ApplicationContext context, int personId)
        {
            Person? e = HandlerUtilites.PersonFinder(context, personId);

            if (e == null)
            {
                return Results.NotFound();
            }

            List<InterestLinkViewModel> result = new List<InterestLinkViewModel>();

            foreach (PersonInterest personInterest in e.PersonInterests)
            {
                foreach (PersonInterestLink personInterestLink in personInterest.Interest.PersonInterestLinks)
                {
                    result.Add(new InterestLinkViewModel
                    {
                        InterestTitle = personInterest.Interest.Title,
                        LinkToInterest = personInterestLink.LinkToInterest,
                    });
                }
            }

            return Results.Json(result);
        }

        // Adds link to interest connected to specific person
        public static IResult AddLinkToInterestOfPerson(ApplicationContext context, int personId, int interestId, PersonInterestLinkDto newLink)
        {
            Person person = HandlerUtilites.PersonFinder(context, personId);
            if (person == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? interest = person.PersonInterests
                .Where(pi => pi.Interest.InterestId == interestId)
                .Select(pi => pi.Interest)
                .SingleOrDefault();

            if (interest == null)
            {
                return Results.NotFound("Interest for person not found");
            }

            // Check if the link already exists
            if (interest.PersonInterestLinks.Any(link => link.LinkToInterest == newLink.LinkToInterest))
            {
                return Results.Conflict("Link already exists for interest");
            }

            PersonInterestLink personInterestLink = new PersonInterestLink
            {
                LinkToInterest = newLink.LinkToInterest
            };

            interest.PersonInterestLinks.Add(personInterestLink);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        // Returns all links to specific interest of specific person
        public static IResult LinksOfInterest(ApplicationContext context, int personId, int interestId)
        {
            Person person = HandlerUtilites.PersonFinder(context, personId);
            if (person == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? interest = person.PersonInterests
                .Where(pi => pi.Interest.InterestId == interestId)
                .Select(pi => pi.Interest)
                .SingleOrDefault();

            if (interest == null)
            {
                return Results.NotFound("Interest for person not found");
            }

            List<PersonInterestLinkViewModel> result = interest.PersonInterestLinks
                .Select(il => new PersonInterestLinkViewModel
                {
                    LinkToInterest = il.LinkToInterest
                })
                .ToList();

            return Results.Json(result);
        }
    }
}
