using MinimalAPIproject.Data;
using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Models;
using MinimalAPIproject.Models.ViewModels;
using System.Net;
using MinimalAPIproject.Models.DTO;
using MinimalAPIproject.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIproject.Handlers
{
    public static class PersonHandler
    {
        // Returns all persons
        public static IResult ListPersons(ApplicationContext context)
        {

            PersonListViewModel[] result = 
                context.Persons
                .Include(p => p.PhoneNumbers)
                .Select(p => new PersonListViewModel()
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

        // Returns specific person with attached phonenumbers, interests and links to interests
        public static IResult ViewPerson(ApplicationContext context, int personId)
        {
            Person? e = HandlerUtilites.PersonFinder(context, personId);

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
                Interests = e.PersonInterests
                    .Where(pi => pi.PersonId == personId)
                    .Select(pi => new InterestViewModel
                    {
                        InterestId = pi.Interest.InterestId,
                        Title = pi.Interest.Title,
                        Description = pi.Interest.Description,
                        Links = pi.Interest.PersonInterestLinks
                            .Where(link => link.PersonId == personId)
                            .Select(link => new PersonInterestLinkViewModel
                            {
                                LinkToInterest = link.LinkToInterest
                            })
                            .ToList()
                    })
                    .ToList()
                };

            return Results.Json(result);
        }

        // Returns persons matching query result
        public static IResult FilterPersons(ApplicationContext context, string query)
        {
            var result = context.Persons
                .Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query))
                .Select(p => new PersonViewModel
                {
                    PersonId = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                })
                .ToList();

            return Results.Json(result);
        }
    }
}
