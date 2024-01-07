﻿using MinimalAPIproject.Data;
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
            // Since internalPersonId is only kept for internal use this converts personId (which is used publically) to the internalPersonId to
            int internalPersonId = personId - 10;

            return context.Persons
                .Where(p => p.InternalPersonId == internalPersonId)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonInterests)
                    .ThenInclude(pi => pi.Interest)
                        .ThenInclude(i => i.InterestLinks)
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
                    Links = pi.Interest.InterestLinks
                        .Where(l => l.InterestId == pi.Interest.InterestId)
                        .Select(l => new InterestLinkViewModel
                        {
                            Link = l.UrlLink
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}