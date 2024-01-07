﻿using MinimalAPIproject.Data;
using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Models;
using MinimalAPIproject.Models.ViewModels;
using System.Net;
using MinimalAPIproject.Models.DTO;
using MinimalAPIproject.Utilities;

namespace MinimalAPIproject.Handlers
{
    public static class PersonHandler
    {
        // Returns all persons
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
                Interests = HandlerUtilites.MapPersonInterests(e.PersonInterests, personId)
            };

            return Results.Json(result);
        }
    }
}
