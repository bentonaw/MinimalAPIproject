using MinimalAPIproject.Data;
using MinimalAPIproject.Models.ViewModels;
using MinimalAPIproject.Models;
using MinimalAPIproject.Models.DTO;
using MinimalAPIproject.Utilities;
using System.Net;

namespace MinimalAPIproject.Handlers
{
    public class PersonInterestHandler
    {
        // Returns interests of specific person, attached links to interest by person is included
        public static IResult ListInterestsOfPerson(ApplicationContext context, int personId)
        {
            Person? e = HandlerUtilites.PersonFinder(context, personId);

            if (e == null)
            {
                return Results.NotFound();
            }

            List<InterestViewModel> result = HandlerUtilites.MapPersonInterests(e.PersonInterests, personId);

            return Results.Json(result);
        }

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
        // Connects person to interest, if interest isn't found new interest is created
        public static IResult ConnectPersonToInterest(ApplicationContext context, int personId, InterestDto interest)
        {
            Person? e = HandlerUtilites.PersonFinder(context, personId);

            if (e == null)
            {
                return Results.NotFound();
            }

            // Check if the interest already exists in the database
            Interest existingInterest = context.Interests.FirstOrDefault(i => i.Title == interest.Title);
            if (existingInterest == null)
            {
                // If the interest doesn't exist, create a new interest
                Interest newInterest = new Interest
                {
                    Title = interest.Title,
                    Description = interest.Description,
                };
                context.Interests.Add(newInterest);

                // Connect the person to the new interest
                PersonInterest personInterest = new PersonInterest { Person = e, Interest = newInterest };
                context.PersonInterests.Add(personInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                // If the interest already exists, connect the person to the existing interest
                PersonInterest personInterest = new PersonInterest { Person = e, Interest = existingInterest };
                context.PersonInterests.Add(personInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
        }

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
    }
}
