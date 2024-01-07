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
        public static IResult ListUrlLinksOfPerson(ApplicationContext context, int personId)
        {
            Person? e = HandlerUtilites.PersonFinder(context, personId);

            if (e == null)
            {
                return Results.NotFound();
            }

            List<InterestLinkViewModel> result = new List<InterestLinkViewModel>();

            foreach (PersonInterest personInterest in e.PersonInterests)
            {
                foreach (InterestLink interestLink in personInterest.Interest.InterestLinks)
                {
                    result.Add(new InterestLinkViewModel
                    {
                        InterestTitle = personInterest.Interest.Title,
                        Link = interestLink.UrlLink
                    });
                }
            }

            return Results.Json(result);
        }

        // Adds link to interest tied to specific person
        public static IResult AddLinkToInterestOfPerson(ApplicationContext context, int personId, string interestTitle, InterestLinkDto newLink)
        {
            Person person = HandlerUtilites.PersonFinder(context, personId);
            if (person == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? interest = person.PersonInterests
                .Where(pi => pi.Interest.Title == interestTitle)
                .Select(pi => pi.Interest)
                .SingleOrDefault();

            if (interest == null)
            {
                return Results.NotFound("Interest for person not found");
            }

            // Check if the link already exists
            if (interest.InterestLinks.Any(link => link.UrlLink == newLink.UrlLink))
            {
                return Results.Conflict("Link already exists for interest");
            }

            InterestLink interestLink = new InterestLink
            {
                UrlLink = newLink.UrlLink
            };

            interest.InterestLinks.Add(interestLink);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        // Returns all links to specific interest of specific person
        public static IResult LinksOfInterest(ApplicationContext context, int personId, string interestTitle)
        {
            Person person = HandlerUtilites.PersonFinder(context, personId);
            if (person == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? interest = person.PersonInterests
                .Where(pi => pi.Interest.Title == interestTitle)
                .Select(pi => pi.Interest)
                .SingleOrDefault();

            if (interest == null)
            {
                return Results.NotFound("Interest for person not found");
            }

            List<InterestLinkViewModel> result = interest.InterestLinks
                .Select(il => new InterestLinkViewModel
                {
                    Link = il.UrlLink
                })
                .ToList();

            return Results.Json(result);
        }

        // Filters all links with search query, interesttitle is included
        public static IResult FilterInterestLinks(ApplicationContext context, string query)
        {
            // Create a queryable representation of the interests in database
            IQueryable<InterestLink> interestLinkQuery = context.InterestLinks.AsQueryable();

            // Filters out interests with query in title.
            IQueryable<InterestLink> filteredInterestLinks = HandlerUtilites.ApplyFilter(interestLinkQuery, query, (interestLink, filter) =>
            interestLink.UrlLink.Contains(filter));

            // Convert the filteredPersons query into a List of PersonViewModel
            List<InterestLinkViewModel> result = filteredInterestLinks
                .Include(il => il.Interest)
                .Select(il => new InterestLinkViewModel
                {
                    InterestTitle = il.Interest.Title,
                    Link = il.UrlLink
                })
                .ToList();

            return Results.Json(result);
        }
    }
}
