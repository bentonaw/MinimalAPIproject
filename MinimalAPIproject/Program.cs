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

            // Returns list of all persons
            app.MapGet("/persons", PersonHandler.ListPersons);

            // Return a view of a specific person, lists everything connected to person
            app.MapGet("/persons/{personId}", PersonHandler.ViewPerson);

            // Return a list of url links connected to person
            app.MapGet("/persons/{personId}/links", PersonInterestHandler.ListUrlLinksOfPerson);

            // Return a list of interests of a specific person
            app.MapGet("/persons/{personId}/interests", PersonInterestHandler.ListInterestsOfPerson);

            // Connects a person to new interest, if interest (by its title) already exists it connects person to said interest
            app.MapPost("/persons/{personId}/interests", PersonInterestHandler.ConnectPersonToInterest);

            // return all interests
            app.MapGet("/interests,");
            // Connects a person to existing specifc interest
            app.MapPost("/interests/{interestId}/", PersonInterestHandler.ConnectPersonToInterest);

            // Returns all links of an interest connected to a specific person
            app.MapPost("/persons/{personId}/{interestId}",);

            // Connect new link to an interest of a specific user
            app.MapPost("/persons/{personId}/{interestId}", PersonInterestHandler.AddLinkToInterestOfPerson);

           
            //// edit person
            ////app.MapPut
            //// edit phonenumber
            //// edit link
            //// edit interest

            //// returns searched for query?

            app.Run();
        }
    }
}
