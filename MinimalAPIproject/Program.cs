using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Data;
using MinimalAPIproject.Handlers;
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

            // Person APIs

            // Returns list of all persons
            app.MapGet("/persons", PersonHandler.ListPersons);

            // Return a list of all persons that include search query in either first or last name
            // example /persons/search?query=John
            app.MapGet("/persons/search", PersonHandler.FilterPersons);

            // Return a view of a specific person, lists everything connected to person
            app.MapGet("/persons/{personId}", PersonHandler.ViewPerson);

            // Interest APIs

            // Return a list of interests of a specific person
            app.MapGet("/persons/{personId}/interests", PersonInterestHandler.ListInterestsOfPerson);

            // Return a list of interests of a specific person that includes search query
            // example /persons/1/interests/search?query=Tennis
            app.MapGet("/persons/{personId}/interests/search", PersonInterestHandler.FilterInterests);

            // Connects a person to new interest, if interest (by its title) already exists it connects person to said interest
            /*{
                "Title": "Sample Interest",
                "Description": "Description of the interest",
            }*/
            app.MapPost("/persons/{personId}/interests", PersonInterestHandler.ConnectPersonToInterest);

            // Links APIs

            // Return a list of url links connected to person
            app.MapGet("/persons/{personId}/links", PersonInterestLinkHandler.ListLinkToInterestsOfPerson);

            // Returns all links of an interest connected to a specific person
            app.MapGet("/persons/{personId}/{interestid}", PersonInterestLinkHandler.LinksOfInterest);

            // Connect new link to an interest of a specific user
            app.MapPost("/persons/{personId}/{interestId}", PersonInterestLinkHandler.AddLinkToInterestOfPerson);

            app.Run();
        }
    }
}
