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

        // Filters interest of person that include search query in either title or description
        public static IResult FilterInterests(ApplicationContext context,int personId, string query)
        {
            var result = context.Persons
                .Where(p => p.PersonId == personId)
                .SelectMany(p => p.PersonInterests)
                .Where(pi => pi.Interest.Title.Contains(query) || pi.Interest.Description.Contains(query))
                .Select(pi => new InterestViewModel
                {
                    InterestId = pi.Interest.InterestId,
                    Title = pi.Interest.Title,
                    Description = pi.Interest.Description,
                    Links = pi.Interest.PersonInterestLinks
                        .Select(link => new PersonInterestLinkViewModel
                            {
                                LinkToInterest = link.LinkToInterest
                            }).ToList()
                })
                .ToList();

            return Results.Json(result);
        }
    }
}
