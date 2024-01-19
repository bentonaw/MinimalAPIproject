using MiniApiProject2.Data;
using MiniApiProject2.Models.ViewModels;
using MiniApiProject2.Models;
using MiniApiProject2.Utilities;
using System.Net;
using MiniApiProject2.Models.Dto;

namespace MiniApiProject2.Handlers
{
    public class InterestHandler
    {
        // Returns interests of specific person, attached links to interest by person is included
        public static IResult ListInterestsOfPerson(MiniApiContext context, int personId)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);

            if (p == null)
            {
                return Results.NotFound();
            }

            var interests = p.Interests
                .Select(i => new InterestViewModel
                {
                    InterestId = i.InterestId,
                    Title = i.Title,
                    Description = i.Description,
                    InterestUrlLinks = i.InterestUrlLinks.Select(l => new InterestUrlLinkViewModel
                    {
                        LinkToInterest = l.LinkToInterest
                    }).ToList(),
                }).ToList();

            return Results.Json(interests);
        }

        // Filters interest of person that include search query in either title or description
        public static IResult FilterInterests(MiniApiContext context, int personId, string query)
        {
            var searchedInterests = context.Persons
                .Where(p => p.PersonId == personId)
                .SelectMany(p => p.Interests)
                .Where(i => i.Title.Contains(query) || i.Description.Contains(query))
                .Select(pi => new InterestViewModel
                {
                    InterestId = pi.InterestId,
                    Title = pi.Title,
                    Description = pi.Description,
                    InterestUrlLinks = pi.InterestUrlLinks
                        .Select(l => new InterestUrlLinkViewModel
                        {
                            LinkToInterest = l.LinkToInterest
                        }).ToList()
                })
                .ToList();

            return Results.Json(searchedInterests);
        }
        // Connects person to interest, if interest isn't found new interest is created
        public static IResult ConnectPersonToInterest(MiniApiContext context, int personId, InterestDto interestDto)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);

            if (p == null)
            {
                return Results.NotFound();
            }

            // Check if the interest already exists in the database
            Interest existingInterest = context.Interests.FirstOrDefault(i => i.Title == interestDto.Title);

            if (existingInterest == null)
            {
                // If the interest doesn't exist, create a new interest
                Interest newInterest = new Interest
                {
                    Title = interestDto.Title,
                    Description = interestDto.Description,
                };
                context.Interests.Add(newInterest);

                // Connect the person to the new interest
                p.Interests.Add(newInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                // If the interest already exists, connect the person to the existing interest
                if (p.Interests.Any(i => i.InterestId == existingInterest.InterestId))
                {
                    // The person is already connected to the interest, return a conflict result
                    return Results.Conflict("Person is already connected to the interest.");
                }

                // Connect the person to the existing interest
                p.Interests.Add(existingInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
        }

        
    }
}
