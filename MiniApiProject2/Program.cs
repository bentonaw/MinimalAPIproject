using Microsoft.EntityFrameworkCore;
using MiniApiProject2.Data;
using MiniApiProject2.Handlers;


//using MiniApiProject2.Data;
//using MiniApiProject2.Handlers;
using MiniApiProject2.Models;
using System.Net;

namespace MiniApiProject2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MiniApiContext");
            builder.Services.AddDbContext<MiniApiContext>(options => options.UseSqlServer(connectionString));
            var app = builder.Build();

            // gets (returns) data
            app.MapGet("/", () => @"Welcome to the miniApiProject by Huan :). The actions available are as follows:

Returns list of all people:
/people

Return a list of all people that include search query in either first or last name
(example: /people/search?query=John):
/people/search

Return a view of a specific person, lists everything connected to person:
/people/{personId}

Add new person
(example: {
    ""firstName"": ""John"",
    ""lastName"": ""Doe""
}):
/people/

Add new phonenumber to person
(example: {
    ""number"": ""123456789""
}):
/people/{personId}

Interest APIs

Return a list of interests of a specific person:
/people/{personId}/interests

Return a list of interests of a specific person that includes search query
(example: /people/1/interests/search?query=Tennis):
/people/{personId}/interests/search

Connects a person to new interest, if interest (by its title) already exists it connects person to that interest:
(example: {
    ""title"": ""Sample Interest"",
    ""description"": ""Description of the interest""
}):
/people/{personId}/interests

Links APIs

Return a list of url links connected to person:
/people/{personId}/links"", InterestUrlLinkHandler.ListInterestUrlLinksOfPerson);

Returns all links of an interest connected to a specific person:
/people/{personId}/interests/{interestId}/links

Connect new link to an interest of a specific user:
(example: {
    ""linkToInterest"": ""samplelink""
}):
/people/{personId}/interests/{interestId}/links
");

            // Person APIs

            // Returns list of all people
            app.MapGet("/people", PersonHandler.ListPeople);

            // Return a list of all people that include search query in either first or last name
            // example /people/search?query=John
            app.MapGet("/people/search", PersonHandler.FilterPeople);

            // Return a view of a specific person, lists everything connected to person
            app.MapGet("/people/{personId}", PersonHandler.ViewPerson);

            // Add new person
            /*{
                "firstName": "John",
                 "lastName": "Doe"
             }*/
            app.MapPost("/people/", PersonHandler.AddNewPerson);

            // Add new phonenumber to person
            /*{
                "number": "123456789"
            }*/
            app.MapPost("/people/{personId}", PersonHandler.AddPhoneNumberToPerson);

            // Interest APIs

            // Return a list of interests of a specific person
            app.MapGet("/people/{personId}/interests", InterestHandler.ListInterestsOfPerson);

            // Return a list of interests of a specific person that includes search query
            // example /people/1/interests/search?query=Tennis
            app.MapGet("/people/{personId}/interests/search", InterestHandler.FilterInterests);

            // Connects a person to new interest, if interest (by its title) already exists it connects person to that interest
            /*{
                "title": "Sample Interest",
                "description": "Description of the interest"
            }*/
            app.MapPost("/people/{personId}/interests", InterestHandler.ConnectPersonToInterest);

            // Links APIs

            // Return a list of url links connected to person
            app.MapGet("/people/{personId}/links", InterestUrlLinkHandler.ListInterestUrlLinksOfPerson);

            // Returns all links of an interest connected to a specific person
            app.MapGet("/people/{personId}/interests/{interestId}/links", InterestUrlLinkHandler.ListInterestUrlLinksOfInterestOfPerson);

            // Connect new link to an interest of a specific user
            /*{
              "linkToInterest": "samplelink"
            }*/
            app.MapPost("/people/{personId}/interests/{interestId}/links", InterestUrlLinkHandler.AddLinkToInterestOfPerson);

            app.Run();
        }
    }
}
