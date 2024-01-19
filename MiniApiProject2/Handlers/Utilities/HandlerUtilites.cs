using Microsoft.EntityFrameworkCore;
using MiniApiProject2.Data;
using MiniApiProject2.Models;


namespace MiniApiProject2.Utilities
{
    public class HandlerUtilites
    {
        // Method for finding person
        public static Person? PersonFinder(MiniApiContext context, int personId)
        {
            return context.Persons
                .Where(p => p.PersonId == personId)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.Interests)
                    .ThenInclude(i => i.InterestUrlLinks)
                .SingleOrDefault();
        }
        // Method for finding interests
        public static Interest? InterestFinder(MiniApiContext context, int interestId)
        {
            return context.Interests
                .Where(i => i.InterestId == interestId)
                .SingleOrDefault();
        }
    }
}
