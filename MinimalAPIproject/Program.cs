using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Data;
using MinimalAPIproject.Models;
using System.Net;

namespace MinimalAPIproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            var app = builder.Build();

            // gets (returns) data
            app.MapGet("/", () => "Hello World!");

            // return all people
            app.MapGet("/people", (ApplicationContext context) =>
            {

                return Results.Json(context.Persons
                    .Select(p => new { p.FirstName, p.LastName,  p.PhoneNumbers})
                    .ToArray());

            });
            // returns specific person, ? to include null. should be{personid} with single or default, om null results.notfound else results.json
            app.MapGet("/people/{name?}", (ApplicationContext context, string? name) =>
            {
                //var interestOfName = context.Persons
                //    .Where(p => p.FirstName+p.LastName == name)
                //    .Select(p => new
                //    {
                //        Person = new { p.FirstName, p.LastName, p.PhoneNumbers },
                //        Interests = p.PersonInterests.Select(pi => pi.Interest).Distinct().ToArray()
                //    })
                //    .ToArray();

                //return Results.Json(interestOfName);

            });
            // edit person
            //app.MapPut
            // edit phonenumber
            // edit link
            // edit interest

            // returns searched for query?

            // returns list of given name and their interests
            app.MapGet("/people/{name}/interests", (ApplicationContext context, string name) =>
            {
                var interestOfName = context.Persons
                    .Where(p => p.FirstName+p.LastName == name)
                    .Select(p => new
                    {
                        Person = new { p.FirstName, p.LastName, p.PhoneNumbers },
                        Interests = p.PersonInterests.Select(pi => pi.Interest).Distinct().ToArray()
                    })
                    .ToArray();

                return Results.Json(interestOfName);

            });
            // returns all links linked to a specific person and interest
            app.MapGet("/people/{name}/{interest}/links", (ApplicationContext context, string name, string interest) =>
            {
                var linksOfInterest = context.Persons
                    .Where(p => $"{p.FirstName}{p.LastName}" == name)
                    .SelectMany(p => p.PersonInterests
                        .Where(pi => pi.Interest.Title == interest)
                        .SelectMany(pi => pi.UrlLink))
                    .Distinct()
                    .ToArray();

                return Results.Json(linksOfInterest);
            });

            // adds an interest to a person
            app.MapPost("/people/{name}/interests", (ApplicationContext context, string name, Interest newInterest) =>
            {
                var person = context.Persons.FirstOrDefault(p => $"{p.FirstName}{p.LastName}" == name);

                if (person != null)
                {
                    //checks if the interest already exists in database
                    var existingInterest = context.Interests.FirstOrDefault(i => i.Title == newInterest.Title);
                    // If the interest doesn't exist, it is created
                    if (existingInterest == null)
                    {
                        existingInterest = newInterest;
                        context.Interests.Add(existingInterest);
                    }

                    var personInterest = new PersonInterest { Person = person, Interest = newInterest };
                    context.PersonInterests.Add(personInterest);

                    context.SaveChanges();
                    return Results.StatusCode((int)HttpStatusCode.Created);
                }
                else
                {
                    return Results.NotFound("Person not found.");
                }
            });

            // add a link for a specific persons interest
            app.MapPost("/people/{name}/{interest}/links", (ApplicationContext context, string name, string interest, InterestLink newLink) =>
            {
                // checks if all criterias are met for link to be added
                // ie. person need exist in db > person need to have interest > adds link if it doesn't exist at persons interest
                var person = context.Persons.FirstOrDefault(p => $"{p.FirstName}{p.LastName}" == name);

                if (person != null)
                {
                    var personInterest = person.PersonInterests.FirstOrDefault(pi => pi.Interest.Title == interest);

                    if (personInterest != null)
                    {
                        //checks if link already exist
                        var existingLink = personInterest.UrlLink.FirstOrDefault(link => link.UrlLink == newLink.UrlLink);

                        // if link doesn't exist, adds it
                        if (existingLink == null)
                        {
                            existingLink = newLink;
                            personInterest.UrlLink.Add(existingLink);
                            context.SaveChanges();
                            return Results.StatusCode((int)HttpStatusCode.Created);
                        }

                        else
                        {
                            // If link already exists, gives a conflicht response that the link already exists
                            return Results.Conflict("Link already exists");
                        }

                    }

                    else
                    {
                        // If interest is not found connected to person, gives a not found respons
                        return Results.NotFound("Interest not found for the person.");
                    }
                }
                else 
                {
                    return Results.NotFound("Person not found") ; 
                }
            
            });

            // add new person
            app.MapPost("/person", (ApplicationContext context, Person person) =>
            {
                context.Persons.Add(person);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });


            app.Run();
        }
    }
}
