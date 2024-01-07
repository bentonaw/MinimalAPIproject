using MinimalAPIproject.Data;
using MinimalAPIproject.Models.ViewModels;
using MinimalAPIproject.Models;
using MinimalAPIproject.Models.DTO;
using MinimalAPIproject.Utilities;
using System.Net;
using Microsoft.EntityFrameworkCore;

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

        // Filters interest of person that include search query
        public static IResult FilterInterest(ApplicationContext context, string query)
        {
            // Create a queryable representation of the interests in database
            IQueryable<Interest> interestQuery = context.Interests.AsQueryable();

            // Filters out interests with query in title.
            IQueryable<Interest> filteredInterests = HandlerUtilites.ApplyFilter(interestQuery, query, (interest, filter) =>
            interest.Title.Contains(filter));

            // Convert the filteredPersons query into a List of PersonViewModel
            List<InterestViewModel> result = filteredInterests
                .Include(i => i.InterestLinks)
                .Select(i => new InterestViewModel
                {
                    Title = i.Title,
                    Description = i.Description,
                    Links = i.InterestLinks
                        .Select(l => new InterestLinkViewModel
                        {
                            Link = l.UrlLink,
                        })
                        .ToList()
                })
                .ToList();

            return Results.Json(result);
        }
    }
}
