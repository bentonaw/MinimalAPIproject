using MiniApiProject2.Data;
using Microsoft.EntityFrameworkCore;
using MiniApiProject2.Models;
using MiniApiProject2.Models.ViewModels;
using MiniApiProject2.Utilities;
using MiniApiProject2.Models.Dto;
using System.Net;
using System.Runtime.ExceptionServices;

namespace MiniApiProject2.Handlers
{

    public static class PersonHandler
    {
        // Returns all persons
        public static IResult ListPeople(MiniApiContext context)
        {
        PersonListViewModel[] listOfUsers =
            context.Persons
            .Include(p => p.PhoneNumbers)
            .Select(p => new PersonListViewModel()
            {
                PersonId = p.PersonId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumbers = p.PhoneNumbers
                    .Select(ph => new PhoneNumberViewModel
                    {
                        Number = ph.Number
                    })
                    .ToList()
            })
            .ToArray();

            return Results.Json(listOfUsers);

        }

        // Returns specific person with attached phonenumbers, interests and links to interests
        public static IResult ViewPerson(MiniApiContext context, int personId)
        {
            //Checks if personId is found in db
            Person? p = HandlerUtilites.PersonFinder(context, personId);

            if(p == null)
            {
                return Results.NotFound();
            }

            // Returns all connected information
            PersonViewModel result = new PersonViewModel()
            {
                PersonId = personId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumbers = p.PhoneNumbers
                    .Where(ph => ph.Person.PersonId == personId)
                    .Select(ph => new PhoneNumberViewModel
                    {
                        Number = ph.Number,
                    })
                    .ToList(),
                // Addded null handling in the segment below.
                // ?? defaults code left of it if null
                // Single ? won't execute the LINQ if null, avoids null exception
                Interests = p.Interests?
                    .Where(i => i.Persons.Any(person => person.PersonId == personId))
                        .Select(pi => new InterestViewModel
                        {
                            Title = pi.Title ?? string.Empty,
                            Description = pi.Description ?? string.Empty,
                            InterestUrlLinks = pi.InterestUrlLinks
                                ?.Where(pil => pil.Person != null && pil.Person.PersonId == personId)
                                .Select(link => new InterestUrlLinkViewModel
                                {
                                    LinkToInterest = link.LinkToInterest
                                })
                                .ToList() ?? new List<InterestUrlLinkViewModel>(), // if null creates empty list of links
                        })
                        .ToList() ?? new List<InterestViewModel>(), // if null creates empty list of interest
            };

            return Results.Json(result);
        }

        // Returns persons matching query result
        public static IResult FilterPeople(MiniApiContext context, string query)
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

        // Adds new person
        public static IResult AddNewPerson(MiniApiContext context, PersonDto personDto)
        {
            // Handling so that both first and last name is required
            if(string.IsNullOrWhiteSpace(personDto.FirstName) || string.IsNullOrWhiteSpace(personDto.LastName)) 
            {
                return Results.BadRequest("First and last name are required");
            }

            // Adds new person with Dto
            var newPerson = new Person
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName
            };

            context.Persons.Add(newPerson);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        // Adds new phonenumber to person
        public static IResult AddPhoneNumberToPerson(MiniApiContext context, int personId, PhoneNumberDto numberDto)
        {
            Person? p = HandlerUtilites.PersonFinder(context, personId);

            if (p == null)
            {
                return Results.NotFound();
            }

            var phoneNumber = new PhoneNumber
            {
                Number = numberDto.Number,
                Person = p
            };

            context.PhoneNumbers.Add(phoneNumber);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
