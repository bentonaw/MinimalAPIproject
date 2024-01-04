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

            app.MapGet("/people", (ApplicationContext context) =>
            {

                return Results.Json(context.Persons.Select(p => new { p.Name, p.PhoneNumbers, p.Interests }).ToArray());

            });
            // add news person
            app.MapPost("/people", (ApplicationContext context, Person person) =>
            {
                context.Persons.Add(person);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });


            app.Run();
        }
    }
}
