using MinimalAPIproject.Data;
using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Models;
using MinimalAPIproject.Models.ViewModels;
using System.Net;

namespace MinimalAPIproject.Handlers
{
    public static class PersonHandler
    {
        public static IResult ListPersons(ApplicationContext context)
        {
            PersonViewModel[] result = 
                context.Persons
                .Include(p => p.PhoneNumbers)
                .Select(p => new PersonViewModel()
                {
                    PersonId = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumbers = p.PhoneNumbers
                        .Select(phone => new PhoneNumberViewModel
                        {
                            PhoneNumberId = phone.PhoneNumberId,
                            Number = phone.Number
                        })
                        .ToList()
                })
                .ToArray();

            return Results.Json(result);
        }

        public static IResult ListInterestsOfPerson(ApplicationContext context, int personId)
        {
            Person? e = PersonFinder(context, personId);

            if (e == null)
            {
                return Results.NotFound();
            }

            List<InterestViewModel> result = MapPersonInterests(e.PersonInterests, personId);

            return Results.Json(result);
        }

        public static IResult ViewPerson(ApplicationContext context, int personId)
        {
            Person? e = PersonFinder(context, personId);

            if(e == null)
            {
                return Results.NotFound();
            }

            PersonViewModel result = new PersonViewModel()
            {
                PersonId = e.PersonId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumbers = e.PhoneNumbers
                    .Where(pn => pn.PersonId == personId)
                    .Select(pn => new PhoneNumberViewModel
                    {
                        PhoneNumberId = pn.PhoneNumberId,
                        Number = pn.Number,
                    })
                    .ToList(),
                Interests = MapPersonInterests(e.PersonInterests, personId)
            };

            return Results.Json(result);
        }

        private static Person? PersonFinder (ApplicationContext context, int personId)
        {
            return context.Persons
                .Where(p => p.PersonId == personId)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonInterests)
                    .ThenInclude(pi => pi.Interest)
                        .ThenInclude(i => i.InterestLinks)
                .SingleOrDefault();
        }

        private static List<InterestViewModel> MapPersonInterests(IEnumerable<PersonInterest> personInterests, int personId)
        {
            return personInterests
                .Where(pi =>pi.PersonId == personId)
                .Select(pi => new InterestViewModel
                {
                    InterestId = pi.Interest.InterestId,
                    Title = pi.Interest.Title,
                    Description = pi.Interest.Description,
                    Links = pi.Interest.InterestLinks
                        .Where(l => l.InterestId == pi.Interest.InterestId)
                        .Select(l => new InterestLinkViewModel
                        {
                            InterestLinkId = l.Interest.InterestId,
                            Link = l.UrlLink
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}
