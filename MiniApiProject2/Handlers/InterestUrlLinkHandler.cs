using MiniApiProject2.Data;
using MiniApiProject2.Models.ViewModels;
using MiniApiProject2.Models;
using MiniApiProject2.Utilities;
using MiniApiProject2.Models.Dto;
using System.Net;

namespace MiniApiProject2.Handlers
{
    public class InterestUrlLinkHandler
    {
        // Returns list of links connected to specific person
        public static IResult ListInterestUrlLinksOfPerson(MiniApiContext context, int personId)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);

            if (p == null)
            {
                return Results.NotFound();
            }

            var interestUrlLinks = p.Interests
                .SelectMany(i => i.InterestUrlLinks)
                .Select(il => new InterestUrlLinkViewModel
                {
                    LinkToInterest = il.LinkToInterest
                })
                .ToList();

            return Results.Json(interestUrlLinks);
        }

        // Returns all links to specific interest of specific person
        public static IResult ListInterestUrlLinksOfInterestOfPerson(MiniApiContext context, int personId, int interestId)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);
            if (p == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? i = HandlerUtilites.InterestFinder(context, interestId);
            if (i == null)
            {
                return Results.NotFound("Interest for person can not be found.");
            }

            var interestUrlLinks = i.InterestUrlLinks
                .Select(il => new InterestUrlLinkViewModel
                {
                    LinkToInterest = il.LinkToInterest
                })
                .ToList();

            return Results.Json(interestUrlLinks);

        }

        // Adds link to interest connected to specific person
        public static IResult AddLinkToInterestOfPerson(MiniApiContext context, int personId, int interestId, InterestUrlLinkDto newLink)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);
            if (p == null)
            {
                return Results.NotFound("Person not found.");
            }

            Interest? i = HandlerUtilites.InterestFinder(context, interestId);
            if (i == null)
            {
                return Results.NotFound("Interest for person can not be found.");
            }

            // Check if the link already exists
            if (context.InterestUrlLinks
                .Any(link => link.LinkToInterest == newLink.LinkToInterest && link.Interest.InterestId==interestId && link.Person.PersonId == personId))
            {
                return Results.Conflict("Person already has link connected to the interest.");
            }

            // Create a new InterestUrlLink entity using the DTO
            var newInterestUrlLink = new InterestUrlLink
            {
                LinkToInterest = newLink.LinkToInterest,
                Person = p,
                Interest = i
            };

            context.InterestUrlLinks.Add(newInterestUrlLink);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }


    }
}
