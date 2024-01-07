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
            PersonViewModel[] result = context.Persons
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
        public static IResult ViewPerson(ApplicationContext context, int personId)
        {
            PersonViewModel result = context.Persons
                .Where(p => p.PersonId == personId)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonInterests)
                    .ThenInclude(pi => pi.Interest)
                            .ThenInclude(i => i.InterestLinks)
                    .Select(p => new PersonViewModel()
                    {
                        PersonId = p.PersonId,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        PhoneNumbers = p.PhoneNumbers
                            .Where(phone => phone.PersonId == p.PersonId)
                            .Select(phone => new PhoneNumberViewModel
                            {
                                PhoneNumberId = phone.PhoneNumberId,
                                Number = phone.Number
                            })
                            .ToList(),
                        Interests = p.PersonInterests
                            .Where(pi => pi.PersonId == p.PersonId)
                            .Select(pi => new InterestViewModel
                            {
                                InterestId = pi.Interest.InterestId,
                                Title = pi.Interest.Title,
                                Description = pi.Interest.Description,
                                Links = pi.Interest.InterestLinks
                                    .Where(link => link.InterestId == pi.Interest.InterestId)
                                    .Select(link => new InterestLinkViewModel
                                    {
                                        InterestLinkId = link.InterestLinkId,
                                        Link = link.UrlLink
                                    })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .SingleOrDefault();
            if (result?.PersonId != null)
            {
                return Results.Json(result);
            }
            else
            {
                return Results.NotFound("Person not found.");
            }

        }
    }
}
